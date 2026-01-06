using FirstDesktopApp.Entities;
using FirstDesktopApp.Level;
using FirstDesktopApp.Rendering;
using FirstDesktopApp.Systems;

namespace FirstDesktopApp.Core
{
    /// <summary>
    /// Main game orchestrator.
    /// </summary>
    public class Game
    {
        private readonly List<GameObject> _objects = new();
        private readonly CollisionSystem _collisionSystem = new();
        private readonly PhysicsSystem _physicsSystem = new();
        
        public Camera? Camera { get; set; }
        public Image? Background { get; set; }
        public Player? Player { get; private set; }
        public List<GameObject> Objects => _objects;
        public float FallDeathY { get; set; } = 800;
        
        // Level completion tracking
        public bool IsLevelComplete { get; private set; }
        public event Action? OnLevelComplete;
        private int _initialEnemyCount;
        private float _levelCompleteTimer;

        public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
            if (obj is Player player)
            {
                Player = player;
                player.OnShoot += SpawnBullet;
            }
            if (obj is Enemy enemy)
            {
                enemy.OnDeath += OnEnemyKilled;
                enemy.OnAttack += SpawnEnemyProjectile;
                _initialEnemyCount++;
            }
        }
        
        public void ResetLevelState()
        {
            IsLevelComplete = false;
            _initialEnemyCount = 0;
            _levelCompleteTimer = 0;
        }

        public void AddObjects(IEnumerable<GameObject> objects)
        {
            foreach (var obj in objects)
                AddObject(obj);
        }

        private void SpawnBullet(Bullet bullet) => _objects.Add(bullet);
        private void SpawnEnemyProjectile(EnemyProjectile proj) => _objects.Add(proj);
        private void OnEnemyKilled(Enemy enemy)
        {
            if (Player != null)
                Player.Score += enemy.ScoreValue;
        }

        public void Update(GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            
            // Handle level complete state - trigger immediately for dialog forms
            if (IsLevelComplete)
            {
                _levelCompleteTimer += dt;
                if (_levelCompleteTimer >= 0.5f) // Short delay for visual feedback
                {
                    OnLevelComplete?.Invoke();
                    _levelCompleteTimer = -999; // Prevent multiple triggers
                }
                return;
            }
            
            var activeObjects = _objects.Where(o => o.IsActive).ToList();
            
            foreach (var obj in activeObjects)
                obj.Update(gameTime);

            // Enemy AI - attack player when in range
            foreach (var enemy in activeObjects.OfType<Enemy>())
                enemy.TryAttack(Player);

            _physicsSystem.Apply(_objects);
            _collisionSystem.Check(_objects);
            CheckPlayerProjectileHits();

            if (Camera != null && Player != null)
                Camera.Follow(Player.Position, Player.Size);

            if (Player != null && !Player.IsDead && Player.Position.Y > FallDeathY)
                Player.Health = 0;

            RemoveOffscreenProjectiles();
            
            // Check for level completion (all enemies dead)
            CheckLevelCompletion();
        }
        
        private void CheckLevelCompletion()
        {
            if (Player == null || Player.IsDead || IsLevelComplete) return;
            
            int enemiesAlive = _objects.OfType<Enemy>().Count(e => e.IsActive && !e.IsDying);
            if (_initialEnemyCount > 0 && enemiesAlive == 0)
            {
                IsLevelComplete = true;
                _levelCompleteTimer = 0;
            }
        }

        private void CheckPlayerProjectileHits()
        {
            if (Player == null || Player.IsDead) return;

            // Check enemy projectiles hitting player
            foreach (var proj in _objects.OfType<EnemyProjectile>().Where(p => p.IsActive).ToList())
            {
                // Use world coordinates for collision
                var projBounds = new RectangleF(proj.Position, proj.Size);
                var playerBounds = new RectangleF(Player.Position, Player.Size);
                
                if (projBounds.IntersectsWith(playerBounds))
                {
                    Player.TakeDamage(proj.Damage);
                    proj.IsActive = false;
                }
            }
            
            // Check player collecting health packs
            foreach (var powerUp in _objects.OfType<PowerUp>().Where(p => p.IsActive).ToList())
            {
                var powerUpBounds = new RectangleF(powerUp.Position, powerUp.Size);
                var playerBounds = new RectangleF(Player.Position, Player.Size);
                
                if (powerUpBounds.IntersectsWith(playerBounds))
                {
                    if (powerUp.Type == PowerUpType.Health)
                    {
                        Player.Health = Math.Min(100, Player.Health + powerUp.HealAmount);
                        Player.Score += 50;
                    }
                    powerUp.IsActive = false;
                }
            }
        }

        private void RemoveOffscreenProjectiles()
        {
            if (Camera == null) return;
            var viewBounds = Camera.GetViewBounds();
            viewBounds.Inflate(300, 300);
            
            foreach (var proj in _objects.Where(o => o is Bullet || o is EnemyProjectile))
            {
                if (!viewBounds.IntersectsWith(proj.Bounds))
                    proj.IsActive = false;
            }
        }

        public void Draw(Graphics g)
        {
            DrawBackground(g);

            foreach (var obj in _objects.Where(o => o.IsActive).OrderBy(o => o is Player ? 1 : 0))
            {
                if (Camera != null)
                {
                    var screenPos = Camera.WorldToScreen(obj.Position);
                    var originalPos = obj.Position;
                    obj.Position = screenPos;
                    obj.Draw(g);
                    obj.Position = originalPos;
                }
                else
                    obj.Draw(g);
            }

            DrawUI(g);
        }

        private void DrawBackground(Graphics g)
        {
            if (Background == null || Camera == null) return;
            float bgOffsetX = -(Camera.Position.X * 0.3f) % Background.Width;
            for (float x = bgOffsetX - Background.Width; x < Camera.ViewportSize.Width + Background.Width; x += Background.Width)
                g.DrawImage(Background, x, 0, Background.Width, Camera.ViewportSize.Height);
        }

        // Current level for display
        public int CurrentLevel { get; set; } = 1;
        
        // Animation timer for dynamic effects
        private float _uiAnimTimer = 0;
        
        private void DrawUI(Graphics g)
        {
            if (Player == null) return;
            
            _uiAnimTimer += 0.05f;
            float viewWidth = Camera?.ViewportSize.Width ?? 1024;
            float viewHeight = Camera?.ViewportSize.Height ?? 640;
            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            
            // ===== TOP HUD BAR WITH GRADIENT =====
            using (var gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, (int)viewWidth, 55),
                Color.FromArgb(230, 15, 15, 30),
                Color.FromArgb(200, 25, 25, 50),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                g.FillRectangle(gradientBrush, 0, 0, viewWidth, 55);
            }
            
            // Glowing bottom border
            using var borderPen = new Pen(Color.FromArgb(180, 80, 120, 200), 2);
            g.DrawLine(borderPen, 0, 55, viewWidth, 55);
            using var glowPen = new Pen(Color.FromArgb(60, 100, 150, 255), 4);
            g.DrawLine(glowPen, 0, 54, viewWidth, 54);
            
            // ----- HEALTH BAR (Left) - Rounded with gradient -----
            DrawHealthBar(g, 15, 10, 200, 28);
            
            // ----- SCORE DISPLAY (Center) - Glowing effect -----
            DrawScoreDisplay(g, viewWidth);
            
            // ----- LEVEL & ENEMIES (Right) - Badge style -----
            DrawLevelBadge(g, viewWidth);
            
            // ===== BOTTOM CONTROLS HINT =====
            DrawControlsHint(g, viewWidth, viewHeight);

            // ===== LEVEL COMPLETE OVERLAY =====
            if (IsLevelComplete)
            {
                DrawLevelCompleteOverlay(g, viewWidth, viewHeight);
            }
            
            // ===== GAME OVER OVERLAY =====
            if (Player.IsDead)
            {
                DrawGameOverOverlay(g, viewWidth, viewHeight);
            }
        }
        
        private void DrawHealthBar(Graphics g, float x, float y, float width, float height)
        {
            float healthPercent = Math.Max(0, Player!.Health) / 100f;
            float fillWidth = width * healthPercent;
            
            // Outer glow for low health
            if (healthPercent < 0.3f)
            {
                float pulse = (float)(Math.Sin(_uiAnimTimer * 4) * 0.3 + 0.7);
                using var glowBrush = new SolidBrush(Color.FromArgb((int)(60 * pulse), 255, 0, 0));
                g.FillRectangle(glowBrush, x - 3, y - 3, width + 6, height + 6);
            }
            
            // Background with rounded corners effect
            using var bgBrush = new SolidBrush(Color.FromArgb(200, 30, 10, 10));
            g.FillRectangle(bgBrush, x, y, width, height);
            
            // Health fill with gradient
            if (fillWidth > 0)
            {
                Color startColor, endColor;
                if (healthPercent > 0.5f)
                {
                    startColor = Color.FromArgb(100, 220, 100);
                    endColor = Color.FromArgb(50, 180, 50);
                }
                else if (healthPercent > 0.25f)
                {
                    startColor = Color.FromArgb(255, 200, 50);
                    endColor = Color.FromArgb(220, 150, 30);
                }
                else
                {
                    startColor = Color.FromArgb(255, 80, 80);
                    endColor = Color.FromArgb(200, 40, 40);
                }
                
                using var fillGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new RectangleF(x, y, fillWidth, height), startColor, endColor,
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                g.FillRectangle(fillGradient, x, y, fillWidth, height);
                
                // Shine effect on top
                using var shineBrush = new SolidBrush(Color.FromArgb(80, 255, 255, 255));
                g.FillRectangle(shineBrush, x, y, fillWidth, height / 3);
            }
            
            // Border
            using var borderPen = new Pen(Color.FromArgb(200, 200, 200), 2);
            g.DrawRectangle(borderPen, x, y, width, height);
            
            // Health icon and text
            using var healthFont = new Font("Segoe UI", 11, FontStyle.Bold);
            string healthText = $"♥ {Player.Health}";
            
            // Shadow
            g.DrawString(healthText, healthFont, Brushes.Black, x + 9, y + 5);
            g.DrawString(healthText, healthFont, Brushes.White, x + 8, y + 4);
        }
        
        private void DrawScoreDisplay(Graphics g, float viewWidth)
        {
            using var scoreFont = new Font("Segoe UI", 18, FontStyle.Bold);
            string scoreText = $"★ {Player!.Score:N0}";
            var scoreSize = g.MeasureString(scoreText, scoreFont);
            float scoreX = (viewWidth - scoreSize.Width) / 2;
            
            // Glowing background box
            float boxPadding = 15;
            var boxRect = new RectangleF(scoreX - boxPadding, 8, scoreSize.Width + boxPadding * 2, 38);
            
            using var boxGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                boxRect, Color.FromArgb(150, 60, 50, 10), Color.FromArgb(150, 40, 35, 5),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(boxGradient, boxRect);
            
            using var boxBorder = new Pen(Color.FromArgb(200, 255, 200, 50), 2);
            g.DrawRectangle(boxBorder, boxRect.X, boxRect.Y, boxRect.Width, boxRect.Height);
            
            // Score text with glow
            using var glowBrush = new SolidBrush(Color.FromArgb(100, 255, 215, 0));
            g.DrawString(scoreText, scoreFont, glowBrush, scoreX + 2, 14);
            g.DrawString(scoreText, scoreFont, Brushes.Gold, scoreX, 12);
        }
        
        private void DrawLevelBadge(Graphics g, float viewWidth)
        {
            int enemiesLeft = _objects.OfType<Enemy>().Count(e => e.IsActive && !e.IsDying);
            
            // Level badge
            using var levelFont = new Font("Segoe UI", 12, FontStyle.Bold);
            using var enemyFont = new Font("Segoe UI", 10, FontStyle.Bold);
            
            string levelText = $"LEVEL {CurrentLevel}";
            string enemiesText = $"⚔ {enemiesLeft} enemies";
            
            var levelSize = g.MeasureString(levelText, levelFont);
            float rightX = viewWidth - 130;
            
            // Badge background
            var badgeRect = new RectangleF(rightX - 10, 6, 125, 42);
            using var badgeGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                badgeRect, Color.FromArgb(180, 30, 60, 90), Color.FromArgb(180, 20, 40, 60),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(badgeGradient, badgeRect);
            
            using var badgeBorder = new Pen(Color.FromArgb(200, 100, 180, 255), 2);
            g.DrawRectangle(badgeBorder, badgeRect.X, badgeRect.Y, badgeRect.Width, badgeRect.Height);
            
            // Level text
            g.DrawString(levelText, levelFont, Brushes.Cyan, rightX, 8);
            
            // Enemies text with color based on count
            Color enemyColor = enemiesLeft == 0 ? Color.LimeGreen : 
                              enemiesLeft <= 2 ? Color.Yellow : Color.White;
            using var enemyBrush = new SolidBrush(enemyColor);
            g.DrawString(enemiesText, enemyFont, enemyBrush, rightX, 28);
        }
        
        private void DrawControlsHint(Graphics g, float viewWidth, float viewHeight)
        {
            using var hintFont = new Font("Segoe UI", 9);
            string hintText = "WASD = Move  |  SPACE = Jump  |  X = Shoot  |  R = Restart";
            var hintSize = g.MeasureString(hintText, hintFont);
            float hintX = (viewWidth - hintSize.Width) / 2;
            float hintY = viewHeight - 28;
            
            // Subtle background
            using var hintBg = new SolidBrush(Color.FromArgb(180, 10, 10, 20));
            g.FillRectangle(hintBg, hintX - 15, hintY - 4, hintSize.Width + 30, hintSize.Height + 8);
            
            using var hintBorder = new Pen(Color.FromArgb(100, 100, 100, 150), 1);
            g.DrawRectangle(hintBorder, hintX - 15, hintY - 4, hintSize.Width + 30, hintSize.Height + 8);
            
            g.DrawString(hintText, hintFont, Brushes.LightGray, hintX, hintY);
        }
        
        private void DrawLevelCompleteOverlay(Graphics g, float viewWidth, float viewHeight)
        {
            // Animated gradient overlay
            float pulse = (float)(Math.Sin(_uiAnimTimer * 2) * 0.1 + 0.9);
            
            using var overlayGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, (int)viewWidth, (int)viewHeight),
                Color.FromArgb((int)(200 * pulse), 0, 60, 30),
                Color.FromArgb((int)(220 * pulse), 0, 100, 50),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(overlayGradient, 0, 0, viewWidth, viewHeight);
            
            // Decorative particles effect (simple circles)
            Random rnd = new Random(42);
            for (int i = 0; i < 20; i++)
            {
                float px = (float)(rnd.NextDouble() * viewWidth);
                float py = (float)(rnd.NextDouble() * viewHeight);
                float size = (float)(5 + Math.Sin(_uiAnimTimer + i) * 3);
                int alpha = (int)(100 + Math.Sin(_uiAnimTimer * 2 + i) * 50);
                using var particleBrush = new SolidBrush(Color.FromArgb(alpha, 150, 255, 150));
                g.FillEllipse(particleBrush, px, py, size, size);
            }
            
            // Main card background
            float cardWidth = 500;
            float cardHeight = 280;
            float cardX = (viewWidth - cardWidth) / 2;
            float cardY = (viewHeight - cardHeight) / 2;
            
            using var cardGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                new RectangleF(cardX, cardY, cardWidth, cardHeight),
                Color.FromArgb(240, 20, 80, 40),
                Color.FromArgb(240, 10, 50, 25),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(cardGradient, cardX, cardY, cardWidth, cardHeight);
            
            // Card border with glow
            using var cardGlow = new Pen(Color.FromArgb(150, 100, 255, 150), 6);
            g.DrawRectangle(cardGlow, cardX, cardY, cardWidth, cardHeight);
            using var cardBorder = new Pen(Color.FromArgb(255, 150, 255, 150), 3);
            g.DrawRectangle(cardBorder, cardX, cardY, cardWidth, cardHeight);
            
            // Title
            using var titleFont = new Font("Segoe UI", 32, FontStyle.Bold);
            string title = "LEVEL COMPLETE!";
            var titleSize = g.MeasureString(title, titleFont);
            float titleX = cardX + (cardWidth - titleSize.Width) / 2;
            
            // Title shadow
            g.DrawString(title, titleFont, Brushes.Black, titleX + 3, cardY + 88);
            g.DrawString(title, titleFont, Brushes.LimeGreen, titleX, cardY + 85);
            
            // Score display
            using var scoreFont = new Font("Segoe UI", 22, FontStyle.Bold);
            string scoreText = $"SCORE: {Player!.Score:N0}";
            var scoreSize = g.MeasureString(scoreText, scoreFont);
            float scoreX = cardX + (cardWidth - scoreSize.Width) / 2;
            
            g.DrawString(scoreText, scoreFont, Brushes.Black, scoreX + 2, cardY + 152);
            g.DrawString(scoreText, scoreFont, Brushes.Gold, scoreX, cardY + 150);
            
            // Level info
            using var levelFont = new Font("Segoe UI", 14);
            string levelInfo = CurrentLevel < 3 ? $"Get ready for Level {CurrentLevel + 1}!" : "Final Level Complete!";
            var levelSize = g.MeasureString(levelInfo, levelFont);
            g.DrawString(levelInfo, levelFont, Brushes.White, 
                cardX + (cardWidth - levelSize.Width) / 2, cardY + 200);
            
            // Continue hint
            using var hintFont = new Font("Segoe UI", 12, FontStyle.Italic);
            string hint = "Loading next level...";
            var hintSize = g.MeasureString(hint, hintFont);
            g.DrawString(hint, hintFont, Brushes.LightGreen, 
                cardX + (cardWidth - hintSize.Width) / 2, cardY + 240);
        }
        
        private void DrawGameOverOverlay(Graphics g, float viewWidth, float viewHeight)
        {
            // Dark red gradient overlay
            float pulse = (float)(Math.Sin(_uiAnimTimer * 3) * 0.1 + 0.9);
            
            using var overlayGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, (int)viewWidth, (int)viewHeight),
                Color.FromArgb((int)(220 * pulse), 60, 0, 0),
                Color.FromArgb((int)(200 * pulse), 30, 0, 0),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(overlayGradient, 0, 0, viewWidth, viewHeight);
            
            // Blood drip effect (simple rectangles)
            Random rnd = new Random(123);
            for (int i = 0; i < 15; i++)
            {
                float dx = (float)(rnd.NextDouble() * viewWidth);
                float dripHeight = (float)(50 + rnd.NextDouble() * 100);
                int alpha = (int)(80 + rnd.NextDouble() * 80);
                using var dripBrush = new SolidBrush(Color.FromArgb(alpha, 150, 0, 0));
                g.FillRectangle(dripBrush, dx, 0, 3, dripHeight);
            }
            
            // Main card
            float cardWidth = 480;
            float cardHeight = 300;
            float cardX = (viewWidth - cardWidth) / 2;
            float cardY = (viewHeight - cardHeight) / 2;
            
            using var cardGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                new RectangleF(cardX, cardY, cardWidth, cardHeight),
                Color.FromArgb(240, 80, 20, 20),
                Color.FromArgb(240, 40, 10, 10),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(cardGradient, cardX, cardY, cardWidth, cardHeight);
            
            // Card border with glow
            using var cardGlow = new Pen(Color.FromArgb(150, 255, 80, 80), 6);
            g.DrawRectangle(cardGlow, cardX, cardY, cardWidth, cardHeight);
            using var cardBorder = new Pen(Color.FromArgb(255, 255, 100, 100), 3);
            g.DrawRectangle(cardBorder, cardX, cardY, cardWidth, cardHeight);
            
            // Title
            using var titleFont = new Font("Segoe UI", 36, FontStyle.Bold);
            string title = "GAME OVER";
            var titleSize = g.MeasureString(title, titleFont);
            float titleX = cardX + (cardWidth - titleSize.Width) / 2;
            
            // Pulsing title
            Color titleColor = Color.FromArgb(255, (int)(200 + Math.Sin(_uiAnimTimer * 4) * 55), 80, 80);
            using var titleBrush = new SolidBrush(titleColor);
            g.DrawString(title, titleFont, Brushes.Black, titleX + 3, cardY + 88);
            g.DrawString(title, titleFont, titleBrush, titleX, cardY + 85);
            
            // Level reached
            using var levelFont = new Font("Segoe UI", 16, FontStyle.Bold);
            string levelText = $"Level {CurrentLevel}";
            var levelSize = g.MeasureString(levelText, levelFont);
            g.DrawString(levelText, levelFont, Brushes.White, 
                cardX + (cardWidth - levelSize.Width) / 2, cardY + 140);
            
            // Final score
            using var scoreFont = new Font("Segoe UI", 20, FontStyle.Bold);
            string scoreText = $"Final Score: {Player!.Score:N0}";
            var scoreSize = g.MeasureString(scoreText, scoreFont);
            
            g.DrawString(scoreText, scoreFont, Brushes.Black, 
                cardX + (cardWidth - scoreSize.Width) / 2 + 2, cardY + 177);
            g.DrawString(scoreText, scoreFont, Brushes.Gold, 
                cardX + (cardWidth - scoreSize.Width) / 2, cardY + 175);
            
            // Restart hint with pulsing effect
            using var hintFont = new Font("Segoe UI", 16, FontStyle.Bold);
            string hint = "Press R to Restart";
            var hintSize = g.MeasureString(hint, hintFont);
            
            int hintAlpha = (int)(180 + Math.Sin(_uiAnimTimer * 5) * 75);
            using var hintBrush = new SolidBrush(Color.FromArgb(hintAlpha, 255, 255, 255));
            g.DrawString(hint, hintFont, hintBrush, 
                cardX + (cardWidth - hintSize.Width) / 2, cardY + 230);
            
            // Motivational message
            using var msgFont = new Font("Segoe UI", 11, FontStyle.Italic);
            string[] messages = { "Don't give up!", "Try again!", "You can do it!", "One more try!" };
            string msg = messages[(int)(_uiAnimTimer / 2) % messages.Length];
            var msgSize = g.MeasureString(msg, msgFont);
            g.DrawString(msg, msgFont, Brushes.LightCoral, 
                cardX + (cardWidth - msgSize.Width) / 2, cardY + 265);
        }

        public void Cleanup() => _objects.RemoveAll(o => !o.IsActive);
    }
}
