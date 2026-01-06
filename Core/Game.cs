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

        private void DrawUI(Graphics g)
        {
            if (Player == null) return;

            g.FillRectangle(Brushes.DarkRed, 10, 10, 200, 25);
            g.FillRectangle(Brushes.Green, 10, 10, Math.Max(0, Player.Health * 2), 25);
            g.DrawRectangle(Pens.White, 10, 10, 200, 25);
            g.DrawString($"Health: {Player.Health}", SystemFonts.DefaultFont, Brushes.White, 15, 14);

            using var scoreFont = new Font("Arial", 14, FontStyle.Bold);
            g.DrawString($"Score: {Player.Score}", scoreFont, Brushes.Yellow, 10, 42);

            int enemiesLeft = _objects.OfType<Enemy>().Count(e => e.IsActive && !e.IsDying);
            g.DrawString($"Enemies: {enemiesLeft}", SystemFonts.DefaultFont, Brushes.White, 10, 65);

            g.DrawString("Arrow/WASD = Move | Space = Jump | X = Shoot | ESC = Menu", 
                SystemFonts.DefaultFont, Brushes.White, 10, Camera?.ViewportSize.Height - 25 ?? 575);

            // Level Complete UI - brief visual feedback before dialog
            if (IsLevelComplete)
            {
                using var font = new Font("Arial", 32, FontStyle.Bold);
                var text = "LEVEL COMPLETE!";
                var size = g.MeasureString(text, font);
                float centerX = (Camera?.ViewportSize.Width ?? 800) / 2 - size.Width / 2;
                float centerY = (Camera?.ViewportSize.Height ?? 600) / 2 - size.Height / 2;
                
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, 0, 80, 0)), 
                    centerX - 20, centerY - 10, size.Width + 40, size.Height + 20);
                g.DrawString(text, font, Brushes.LimeGreen, centerX, centerY);
            }
        }

        public void Cleanup() => _objects.RemoveAll(o => !o.IsActive);
    }
}
