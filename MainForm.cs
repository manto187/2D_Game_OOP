using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Level;
using FirstDesktopApp.Movements;
using FirstDesktopApp.Rendering;
using EZInput;

namespace FirstDesktopApp
{
    public partial class MainForm : Form
    {
        // Game state
        private int _highestUnlockedLevel = 1;
        private int _currentLevel = 1;
        private int _playerScore = 0;
        
        // Game components
        private Game? _game;
        private GameTime? _gameTime;
        private System.Windows.Forms.Timer? _gameTimer;
        private BufferedGraphicsContext? _context;
        private BufferedGraphics? _buffer;
        private LevelLoader? _levelLoader;
        private string? _resourcePath;
        
        // Fullscreen state
        private bool _isFullscreen = false;
        private FormWindowState _previousWindowState;
        private FormBorderStyle _previousBorderStyle;
        private Size _previousSize;
        
        // Death handling
        private bool _deathHandled = false;

        public MainForm()
        {
            InitializeComponent();
            SetupForm();
            ShowMainMenu();
        }

        private void SetupForm()
        {
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
            this.Resize += MainForm_Resize;
            
            _context = BufferedGraphicsManager.Current;
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape && panelGame.Visible)
            {
                ShowPauseOptions();
                e.Handled = true;
            }
        }

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            if (panelGame.Visible && ClientSize.Width > 0 && ClientSize.Height > 0)
            {
                RecreateBuffer();
            }
        }

        private void ToggleFullscreen()
        {
            if (_isFullscreen)
            {
                FormBorderStyle = _previousBorderStyle;
                WindowState = _previousWindowState;
                ClientSize = _previousSize;
                _isFullscreen = false;
            }
            else
            {
                _previousBorderStyle = FormBorderStyle;
                _previousWindowState = WindowState;
                _previousSize = ClientSize;
                
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                _isFullscreen = true;
            }
        }

        #region Panel Visibility Management
        
        private void HideAllPanels()
        {
            panelMainMenu.Visible = false;
            panelLevelSelect.Visible = false;
            panelInstructions.Visible = false;
            panelGame.Visible = false;
            panelLevelComplete.Visible = false;
            panelLevelFailed.Visible = false;
        }

        private void ShowMainMenu()
        {
            StopGame();
            HideAllPanels();
            panelMainMenu.Visible = true;
            panelMainMenu.BringToFront();
            this.Text = "Shadow Hunter - Main Menu";
        }

        private void ShowLevelSelect()
        {
            HideAllPanels();
            UpdateLevelButtons();
            panelLevelSelect.Visible = true;
            panelLevelSelect.BringToFront();
            this.Text = "Shadow Hunter - Level Select";
        }

        private void ShowInstructions()
        {
            HideAllPanels();
            panelInstructions.Visible = true;
            panelInstructions.BringToFront();
            this.Text = "Shadow Hunter - Instructions";
        }

        private void ShowGame()
        {
            HideAllPanels();
            panelGame.Visible = true;
            panelGame.BringToFront();
            this.Text = $"Shadow Hunter - Level {_currentLevel}";
            
            InitializeGame();
            StartGameLoop();
        }

        private void ShowLevelComplete()
        {
            StopGame();
            HideAllPanels();
            
            lblCompleteLevel.Text = $"Level {_currentLevel} Complete!";
            lblCompleteScore.Text = $"Score: {_playerScore}";
            
            bool hasNextLevel = _currentLevel < 3;
            btnNextLevel.Visible = hasNextLevel;
            btnNextLevel.Enabled = hasNextLevel;
            
            panelLevelComplete.Visible = true;
            panelLevelComplete.BringToFront();
            this.Text = "Shadow Hunter - Level Complete!";
        }

        private void ShowLevelFailed()
        {
            StopGame();
            HideAllPanels();
            
            lblFailedLevel.Text = $"Level {_currentLevel}";
            lblFailedScore.Text = $"Score: {_playerScore}";
            
            panelLevelFailed.Visible = true;
            panelLevelFailed.BringToFront();
            this.Text = "Shadow Hunter - Game Over";
        }

        #endregion

        #region Level Selection

        private void UpdateLevelButtons()
        {
            btnLevel1.Enabled = true;
            btnLevel2.Enabled = _highestUnlockedLevel >= 2;
            btnLevel3.Enabled = _highestUnlockedLevel >= 3;

            // Visual feedback for locked levels
            btnLevel2.BackColor = _highestUnlockedLevel >= 2 ? 
                Color.FromArgb(60, 60, 100) : Color.FromArgb(40, 40, 50);
            btnLevel3.BackColor = _highestUnlockedLevel >= 3 ? 
                Color.FromArgb(60, 60, 100) : Color.FromArgb(40, 40, 50);
            
            btnLevel2.ForeColor = _highestUnlockedLevel >= 2 ? Color.White : Color.FromArgb(100, 100, 100);
            btnLevel3.ForeColor = _highestUnlockedLevel >= 3 ? Color.White : Color.FromArgb(100, 100, 100);

            lblLock2.Visible = _highestUnlockedLevel < 2;
            lblLock3.Visible = _highestUnlockedLevel < 3;
        }

        #endregion

        #region Game Logic

        private void RecreateBuffer()
        {
            _buffer?.Dispose();
            if (_context != null && panelGame.Width > 0 && panelGame.Height > 0)
            {
                _buffer = _context.Allocate(panelGame.CreateGraphics(), panelGame.ClientRectangle);
                
                if (_game?.Camera != null)
                {
                    _game.Camera.ViewportSize = new SizeF(panelGame.Width, panelGame.Height);
                }
            }
        }

        private void InitializeGame()
        {
            _deathHandled = false;
            _game = new Game();
            _gameTime = new GameTime();
            
            _game.OnLevelComplete += OnLevelComplete;

            _resourcePath = Path.Combine(Application.StartupPath, "Resources");
            _levelLoader = new LevelLoader(_resourcePath);
            
            var levelData = _currentLevel switch
            {
                3 => Level3.Create(),
                2 => Level2.Create(),
                _ => Level1.Create()
            };

            _game.Camera = new Camera(new SizeF(panelGame.Width, panelGame.Height))
            {
                WorldBounds = new RectangleF(0, 0, levelData.Width * levelData.TileSize, levelData.Height * levelData.TileSize)
            };
            _game.FallDeathY = levelData.FallDeathY;

            _game.Background = _levelLoader.GetBackgroundImage(levelData.TilesetName);

            var tiles = _levelLoader.BuildTiles(levelData);
            _game.AddObjects(tiles.Cast<GameObject>());

            var obstacles = _levelLoader.BuildObstacles(levelData);
            _game.AddObjects(obstacles.Cast<GameObject>());

            var healthPacks = _levelLoader.BuildHealthPacks(levelData);
            _game.AddObjects(healthPacks.Cast<GameObject>());

            var spawnY = FindGroundY(levelData, levelData.PlayerSpawn.X);
            var player = new Player
            {
                Position = new PointF(levelData.PlayerSpawn.X, spawnY),
                AnimatedSprite = SpriteLoader.LoadPlayerSprites(_resourcePath),
                Movement = new KeyboardMovement { Speed = 6f, JumpForce = -14f, IsGrounded = true }
            };
            player.Score = _playerScore;
            _game.AddObject(player);

            CreateEnemies(levelData);
            RecreateBuffer();
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
                
                _levelLoader?.ApplyWraithAnimations(enemy, spawn.EnemyType);
                _game?.AddObject(enemy);
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

        private void StartGameLoop()
        {
            _gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        private void StopGame()
        {
            _gameTimer?.Stop();
            _gameTimer?.Dispose();
            _gameTimer = null;
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            if (_game == null || _gameTime == null) return;
            
            _game.Update(_gameTime);
            _game.Cleanup();
            
            if (_game.Player?.IsDead == true && !_game.IsLevelComplete && !_deathHandled)
            {
                _deathHandled = true;
                _playerScore = _game.Player?.Score ?? 0;
                ShowLevelFailed();
                return;
            }
            
            Render();
        }

        private void OnLevelComplete()
        {
            _playerScore = _game?.Player?.Score ?? 0;
            
            if (_currentLevel >= _highestUnlockedLevel && _currentLevel < 3)
            {
                _highestUnlockedLevel = _currentLevel + 1;
            }
            
            ShowLevelComplete();
        }

        private void Render()
        {
            if (_buffer == null || _game == null) return;
            
            var g = _buffer.Graphics;
            g.Clear(Color.CornflowerBlue);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            _game.Draw(g);
            
            _buffer.Render();
        }

        private void ShowPauseOptions()
        {
            _gameTimer?.Stop();
            
            var result = MessageBox.Show(
                "Game Paused\n\nReturn to Main Menu?",
                "Paused",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _playerScore = 0;
                ShowMainMenu();
            }
            else
            {
                _gameTimer?.Start();
            }
        }

        #endregion

        #region Instructions Panel Paint

        private void panelInstructionsContent_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            float y = 20;
            float x = 30;
            float lineHeight = 32;
            
            using var headerFont = new Font("Segoe UI", 14, FontStyle.Bold);
            using var textFont = new Font("Segoe UI", 11);
            using var headerBrush = new SolidBrush(Color.FromArgb(255, 200, 50));
            using var textBrush = new SolidBrush(Color.White);
            using var keyBrush = new SolidBrush(Color.FromArgb(100, 150, 255));
            
            // Movement
            g.DrawString("MOVEMENT", headerFont, headerBrush, x, y);
            y += lineHeight;
            g.DrawString("[Arrow Keys / WASD] - Move Left & Right", textFont, textBrush, x, y);
            y += lineHeight;
            g.DrawString("[Space / W / Up Arrow] - Jump", textFont, textBrush, x, y);
            y += lineHeight * 1.5f;
            
            // Combat
            g.DrawString("COMBAT", headerFont, headerBrush, x, y);
            y += lineHeight;
            g.DrawString("[X / Ctrl] - Shoot", textFont, textBrush, x, y);
            y += lineHeight * 1.5f;
            
            // Objective
            g.DrawString("OBJECTIVE", headerFont, headerBrush, x, y);
            y += lineHeight;
            g.DrawString("• Defeat all enemies to complete each level", textFont, textBrush, x, y);
            y += lineHeight;
            g.DrawString("• Collect health packs to restore health", textFont, textBrush, x, y);
            y += lineHeight;
            g.DrawString("• Avoid enemy projectiles and falling off platforms", textFont, textBrush, x, y);
            y += lineHeight * 1.5f;
            
            // Tips
            g.DrawString("TIPS", headerFont, headerBrush, x, y);
            y += lineHeight;
            g.DrawString("• Higher levels have faster and stronger enemies", textFont, textBrush, x, y);
            y += lineHeight;
            g.DrawString("• Your score carries over between levels", textFont, textBrush, x, y);
            y += lineHeight;
            g.DrawString("• Press ESC during gameplay to pause", textFont, textBrush, x, y);
            y += lineHeight;
            g.DrawString("• Press F11 for fullscreen mode", textFont, textBrush, x, y);
        }

        #endregion

        #region Button Click Handlers

        // Main Menu buttons
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            _currentLevel = _highestUnlockedLevel;
            _playerScore = 0;
            ShowGame();
        }

        private void btnLevelSelect_Click(object sender, EventArgs e)
        {
            ShowLevelSelect();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            ShowInstructions();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Level Select buttons
        private void btnLevel1_Click(object sender, EventArgs e)
        {
            _currentLevel = 1;
            _playerScore = 0;
            ShowGame();
        }

        private void btnLevel2_Click(object sender, EventArgs e)
        {
            if (_highestUnlockedLevel >= 2)
            {
                _currentLevel = 2;
                _playerScore = 0;
                ShowGame();
            }
        }

        private void btnLevel3_Click(object sender, EventArgs e)
        {
            if (_highestUnlockedLevel >= 3)
            {
                _currentLevel = 3;
                _playerScore = 0;
                ShowGame();
            }
        }

        private void btnBackFromLevelSelect_Click(object sender, EventArgs e)
        {
            ShowMainMenu();
        }

        // Instructions button
        private void btnBackFromInstructions_Click(object sender, EventArgs e)
        {
            ShowMainMenu();
        }

        // Level Complete buttons
        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            _currentLevel++;
            ShowGame();
        }

        private void btnReplayLevel_Click(object sender, EventArgs e)
        {
            ShowGame();
        }

        private void btnCompleteToMenu_Click(object sender, EventArgs e)
        {
            _playerScore = 0;
            ShowMainMenu();
        }

        // Level Failed buttons
        private void btnRetryLevel_Click(object sender, EventArgs e)
        {
            _playerScore = 0;
            ShowGame();
        }

        private void btnFailedToMenu_Click(object sender, EventArgs e)
        {
            _playerScore = 0;
            ShowMainMenu();
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopGame();
            _buffer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
