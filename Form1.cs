using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Level;
using FirstDesktopApp.Movements;
using FirstDesktopApp.Rendering;
using EZInput;

namespace FirstDesktopApp
{
    public partial class Form1 : Form
    {
        private Game _game = null!;
        private GameTime _gameTime = null!;
        private System.Windows.Forms.Timer _gameTimer = null!;
        private BufferedGraphicsContext _context = null!;
        private BufferedGraphics _buffer = null!;
        private LevelLoader _levelLoader = null!;
        private string _resourcePath = null!;
        private int _currentLevel = 1;
        private int _playerScore = 0;

        public Form1()
        {
            InitializeComponent();
            SetupForm();
            InitializeGame();
            StartGameLoop();
           
        }

        private void SetupForm()
        {
            Text = "Platformer Game - Level 1 | X/Ctrl to Shoot";
            ClientSize = new Size(1024, 640);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.CornflowerBlue;
            KeyPreview = true;
            KeyDown += Form1_KeyDown;

            _context = BufferedGraphicsManager.Current;
            _buffer = _context.Allocate(CreateGraphics(), ClientRectangle);
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            // Restart game on R key when dead
            if (e.KeyCode == Keys.R && _game?.Player?.IsDead == true)
            {
                _currentLevel = 1;
                _playerScore = 0;
                InitializeGame();
            }
        }

        private void InitializeGame()
        {
            _game = new Game();
            _gameTime = new GameTime();
            
            // Subscribe to level complete event
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
            
            Text = $"Platformer Game - Level {_currentLevel} | X/Ctrl to Shoot";

            // Setup camera
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

            // Build health packs (Level 2 and 3)
            var healthPacks = _levelLoader.BuildHealthPacks(levelData);
            _game.AddObjects(healthPacks.Cast<GameObject>());

            // Create player - spawn on ground
            var spawnY = FindGroundY(levelData, levelData.PlayerSpawn.X);
            var player = new Player
            {
                Position = new PointF(levelData.PlayerSpawn.X, spawnY),
                AnimatedSprite = SpriteLoader.LoadPlayerSprites(_resourcePath),
                Movement = new KeyboardMovement { Speed = 6f, JumpForce = -14f, IsGrounded = true }
            };
            player.Score = _playerScore; // Carry over score from previous level
            _game.AddObject(player);

            // Create enemies
            foreach (var spawn in levelData.EnemySpawns)
            {
                var enemyType = spawn.EnemyType switch
                {
                    "Wraith_02" => EnemyType.Wraith02,
                    "Wraith_03" => EnemyType.Wraith03,
                    _ => EnemyType.Wraith01
                };

                // Level 3 uses aggressive AI with faster speeds
                IMovement movement;
                if (spawn.IsAggressive || _currentLevel == 3)
                {
                    // Aggressive enemies for Level 3 - faster and chase player
                    float aggressiveSpeed = enemyType switch
                    {
                        EnemyType.Wraith03 => 3.5f,
                        EnemyType.Wraith02 => 3.0f,
                        _ => 2.5f
                    };
                    movement = new AggressiveAIMovement(spawn.PatrolLeft, spawn.PatrolRight, aggressiveSpeed, 280, 450);
                }
                else
                {
                    // Normal random AI for Level 1 and 2
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
                
                // Make Level 3 enemies more aggressive with faster attacks
                if (_currentLevel == 3)
                {
                    enemy.AttackCooldown = 0.5f; // Very fast attacks
                    enemy.AttackRange = 500f;   // Longer range
                }
                
                _levelLoader.ApplyWraithAnimations(enemy, spawn.EnemyType);
                _game.AddObject(enemy);
            }
        }
        
        private float FindGroundY(LevelData levelData, float x)
        {
            int col = (int)(x / levelData.TileSize);
            col = Math.Clamp(col, 0, levelData.Width - 1);
            
            // Find the first solid tile from top to bottom
            for (int row = 0; row < levelData.Height; row++)
            {
                if (levelData.TileMap[row, col] > 0)
                {
                    // Return position just above this tile (minus player height of 80)
                    return row * levelData.TileSize - 80;
                }
            }
            
            // Default spawn if no ground found
            return levelData.PlayerSpawn.Y;
        }
        
        private void OnLevelComplete()
        {
            // Save player score before transitioning
            _playerScore = _game.Player?.Score ?? 0;
            
            // Move to next level
            _currentLevel++;
            
            if (_currentLevel > 3)
            {
                // Game completed - show victory and reset
                MessageBox.Show($"Congratulations! You completed all levels!\nFinal Score: {_playerScore}", 
                    "Victory!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _currentLevel = 1;
                _playerScore = 0;
            }
            
            InitializeGame();
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
            Render();
        }

        private void Render()
        {
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
