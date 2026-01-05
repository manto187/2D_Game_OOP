using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// Projectile fired by enemies.
    /// </summary>
    public class EnemyProjectile : GameObject, ICollidable
    {
        public int Damage { get; set; } = 20;
        public float Speed { get; set; } = 10f;
        public bool MovingRight { get; set; }
        private float _lifetime = 5f;

        public EnemyProjectile(float x, float y, bool movingRight)
        {
            Position = new PointF(x, y);
            Size = new SizeF(25, 25);
            MovingRight = movingRight;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            float dx = MovingRight ? Speed : -Speed;
            Position = new PointF(Position.X + dx, Position.Y);
            
            _lifetime -= dt;
            if (_lifetime <= 0)
                IsActive = false;
        }

        public override void Draw(Graphics g)
        {
            // Draw as a glowing magical orb
            using var outerBrush = new SolidBrush(Color.FromArgb(180, 100, 0, 200));
            g.FillEllipse(outerBrush, Bounds);
            
            using var middleBrush = new SolidBrush(Color.FromArgb(220, 150, 50, 255));
            g.FillEllipse(middleBrush, Position.X + 3, Position.Y + 3, Size.Width - 6, Size.Height - 6);
            
            using var innerBrush = new SolidBrush(Color.FromArgb(255, 220, 150, 255));
            g.FillEllipse(innerBrush, Position.X + 7, Position.Y + 7, Size.Width - 14, Size.Height - 14);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Level.Tile || other is Level.Obstacle)
                IsActive = false;
        }
    }
}
