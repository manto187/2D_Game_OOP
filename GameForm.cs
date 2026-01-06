using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Level;
using FirstDesktopApp.Movements;
using FirstDesktopApp.Rendering;
using EZInput;

namespace FirstDesktopApp
{
    public partial class GameForm : Form
    {
        private Game _game = null!;
        private GameTime _gameTime = null!;
        private System.Windows.Forms.Timer _gameTimer = null!;
        private BufferedGraphicsContext _context = null!;
        private BufferedGraphics _buffer = null!;
        private LevelLoader _levelLoader = null!;
        private string _resourcePath = null!;
        private int _currentLevel;
        private int _playerScore = 0;
        private bool _isFullscreen = false;
        private FormWindowState _previousWindowState;
        private FormBorderStyle _previousBorderStyle;
        private Size _previousSize;
        
        public int HighestUnlockedLevel { get; private set; }
        public bool ShouldReturnToMenu { get; private set; } = false;

        public GameForm(int startLevel = 1, int highestUnlockedLevel = 1)
        {
            InitializeComponent();
            _currentLevel = startLevel;
            HighestUnlockedLevel = highestUnlockedLevel;
            SetupForm();
            InitializeGame();
            StartGameLoop();
        }

        private void SetupForm()
        {
            Text = $"Shadow Hunter - Level {_currentLevel}";
            ClientSize = new Size(1024, 640);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.CornflowerBlue;
            KeyPreview = true;
            KeyDown += GameForm_KeyDown;
            Resize += GameForm_Resize;

            _context = BufferedGraphicsManager.Current;
            RecreateBuffer();
        }

        private void RecreateBuffer()
        {
            _buffer?.Dispose();
            _buffer = _context.Allocate(CreateGraphics(), ClientRectangle);
            
            // Update camera viewport if game exists
            if (_game?.Camera != null)
            {
                _game.Camera.ViewportSize = new SizeF(ClientSize.Width, ClientSize.Height);
            }
        }

        private void GameForm_Resize(object? sender, EventArgs e)
        {
            if (ClientSize.Width > 0 && ClientSize.Height > 0)
            {
                RecreateBuffer();
            }
        }

        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            // F11 for fullscreen toggle
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
                e.Handled = true;
            }
            // Escape to pause/return to menu
            else if (e.KeyCode == Keys.Escape)
            {
                ShowPauseMenu();
                e.Handled = true;
            }
        }

        private void ToggleFullscreen()
        {
            if (_isFullscreen)
            {
                // Exit fullscreen
                FormBorderStyle = _previousBorderStyle;
                WindowState = _previousWindowState;
                ClientSize = _previousSize;
                _isFullscreen = false;
            }
            else
            {
                // Enter fullscreen
                _previousBorderStyle = FormBorderStyle;
                _previousWindowState = WindowState;
                _previousSize = ClientSize;
                
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                _isFullscreen = true;
            }
        }

        private void ShowPauseMenu()
        {
            _gameTimer?.Stop();
            
            var result = MessageBox.Show(
                "Game Paused\n\nDo you want to return to the main menu?",
                "Paused",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                ShouldReturnToMenu = true;
                this.Close();
            }
            else
            {
                _gameTimer?.Start();
            }
        }

        private void InitializeGame()
        {
            _game = new Game();
            _gameTime = new GameTime();
            
            // Subscribe to level complete event - but don't auto-advance
            _game.OnLevelComplete += OnLevelComplete;

            _resourcePath = Path.Combine(Application.StartupPath, "Resources");
            _levelLoader = new LevelLoader(_resourcePath);
            
            // Load appropriate level
            var levelData = _currentLevel switch
            {
                3 => Level3.Create(),
                2 => Level2.Create(),
                _ => Level1.Create()
            };
            
            Text = $"Shadow Hunter - Level {_currentLevel}";

            // Setup camera with current viewport size
            _game.Camera = new Camera(new SizeF(ClientSize.Width, ClientSize.Height))
            {
                WorldBounds = new RectangleF(0, 0, levelData.Width * levelData.TileSize, levelData.Height * levelData.TileSize)
            };
            _game.FallDeathY = levelData.FallDeathY;

            // Load background based on level tileset
            _game.Background = _levelLoader.GetBackgroundImage(levelData.TilesetName);

            // Build level tiles
            var tiles = _levelLoader.BuildTiles(levelData);
            _game.AddObjects(tiles.Cast<GameObject>());

            // Build obstacles
            var obstacles = _levelLoader.BuildObstacles(levelData);
            _game.AddObjects(obstacles.Cast<GameObject>());

            // Build health packs
            var healthPacks = _levelLoader.BuildHealthPacks(levelData);
            _game.AddObjects(healthPacks.Cast<GameObject>());

            // Create player
            var spawnY = FindGroundY(levelData, levelData.PlayerSpawn.X);
            var player = new Player
            {
                Position = new PointF(levelData.PlayerSpawn.X, spawnY),
                AnimatedSprite = SpriteLoader.LoadPlayerSprites(_resourcePath),
                Movement = new KeyboardMovement { Speed = 6f, JumpForce = -14f, IsGrounded = true }
            };
            player.Score = _playerScore;
            _game.AddObject(player);

            // Create enemies
            CreateEnemies(levelData);
        }

        private void CreateEnemies(LevelData levelData)
        {
            foreach (var spawn in levelData.EnemySpawns)
            {
                var enemyType = spawn.EnemyType switch
                {
                    "Wraith_02" => EnemyType.Wraith02,
                    "Wraith_03" => EnemyType.Wraith03,
                    _ => EnemyType.Wraith01
                };

                float levelSpeedMultiplier = _currentLevel switch
                {
                    3 => 2.0f,
                    2 => 1.3f,
                    _ => 1.0f
                };

                IMovement movement;
                
                if (_currentLevel >= 2)
                {
                    float baseSpeed = enemyType switch
                    {
                        EnemyType.Wraith03 => 2.5f,
                        EnemyType.Wraith02 => 2.0f,
                        _ => 1.5f
                    };
                    
                    float finalSpeed = baseSpeed * levelSpeedMultiplier;
                    float minY = _currentLevel == 3 ? 250 : 300;
                    float maxY = _currentLevel == 3 ? 420 : 400;
                    
                    movement = new AggressiveAIMovement(spawn.PatrolLeft, spawn.PatrolRight, finalSpeed, minY, maxY);
                }
                else
                {
                    float speed = enemyType switch
                    {
                        EnemyType.Wraith03 => 2.0f,
                        EnemyType.Wraith02 => 1.8f,
                        _ => 1.5f
                    };
                    movement = new RandomAIMovement(spawn.PatrolLeft, spawn.PatrolRight, speed);
                }

                var enemy = new Enemy(enemyType)
                {
                    Position = new PointF(spawn.X, spawn.Y),
                    Movement = movement
                };
                
                if (_currentLevel == 3)
                {
                    enemy.AttackCooldown = 0.4f;
                    enemy.AttackRange = 550f;
                }
                else if (_currentLevel == 2)
                {
                    enemy.AttackCooldown = 0.8f;
                    enemy.AttackRange = 400f;
                }
                
                _levelLoader.ApplyWraithAnimations(enemy, spawn.EnemyType);
                _game.AddObject(enemy);
            }
        }
        
        private float FindGroundY(LevelData levelData, float x)
        {
            int col = (int)(x / levelData.TileSize);
            col = Math.Clamp(col, 0, levelData.Width - 1);
            
            for (int row = 0; row < levelData.Height; row++)
            {
                if (levelData.TileMap[row, col] > 0)
                {
                    return row * levelData.TileSize - 80;
                }
            }
            
            return levelData.PlayerSpawn.Y;
        }
        
        private void OnLevelComplete()
        {
            _gameTimer?.Stop();
            _playerScore = _game.Player?.Score ?? 0;
            
            bool hasNextLevel = _currentLevel < 3;
            
            // Unlock next level
            if (_currentLevel >= HighestUnlockedLevel && hasNextLevel)
            {
                HighestUnlockedLevel = _currentLevel + 1;
            }
            
            using var completeForm = new LevelCompleteForm(_currentLevel, _playerScore, hasNextLevel);
            var result = completeForm.ShowDialog();
            
            switch (completeForm.SelectedAction)
            {
                case LevelCompleteForm.LevelCompleteAction.NextLevel:
                    _currentLevel++;
                    InitializeGame();
                    _gameTimer?.Start();
                    break;
                    
                case LevelCompleteForm.LevelCompleteAction.Replay:
                    InitializeGame();
                    _gameTimer?.Start();
                    break;
                    
                case LevelCompleteForm.LevelCompleteAction.MainMenu:
                    ShouldReturnToMenu = true;
                    this.Close();
                    break;
            }
        }

        private void StartGameLoop()
        {
            _gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            _game.Update(_gameTime);
            _game.Cleanup();
            
            // Check for player death
            if (_game.Player?.IsDead == true && !_game.IsLevelComplete)
            {
                HandlePlayerDeath();
            }
            
            Render();
        }

        private bool _deathHandled = false;
        
        private void HandlePlayerDeath()
        {
            if (_deathHandled) return;
            _deathHandled = true;
            
            _gameTimer?.Stop();
            int finalScore = _game.Player?.Score ?? 0;
            
            using var failedForm = new LevelFailedForm(_currentLevel, finalScore);
            var result = failedForm.ShowDialog();
            
            switch (failedForm.SelectedAction)
            {
                case LevelFailedForm.LevelFailedAction.Retry:
                    _playerScore = 0; // Reset score on retry
                    _deathHandled = false;
                    InitializeGame();
                    _gameTimer?.Start();
                    break;
                    
                case LevelFailedForm.LevelFailedAction.MainMenu:
                    ShouldReturnToMenu = true;
                    this.Close();
                    break;
            }
        }

        private void Render()
        {
            if (_buffer == null) return;
            
            var g = _buffer.Graphics;
            g.Clear(Color.CornflowerBlue);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            _game.Draw(g);
            
            _buffer.Render();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _gameTimer?.Stop();
            _buffer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
