using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// Projectile fired by enemies.
    /// </summary>
    public class EnemyProjectile : GameObject, ICollidable
    {
        public int Damage { get; set; } = 15;
        public float Speed { get; set; } = 8f;
        public bool MovingRight { get; set; }
        private float _lifetime = 3f;

        public EnemyProjectile(float x, float y, bool movingRight)
        {
            Position = new PointF(x, y);
            Size = new SizeF(20, 20);
            MovingRight = movingRight;
        }

        public override void Update(GameTime gameTime)
        {
            float dx = MovingRight ? Speed : -Speed;
            Position = new PointF(Position.X + dx, Position.Y);
            
            _lifetime -= gameTime.DeltaTime / 60f;
            if (_lifetime <= 0)
                IsActive = false;
        }

        public override void Draw(Graphics g)
        {
            // Draw as a glowing orb
            using var brush = new SolidBrush(Color.FromArgb(200, 150, 0, 255));
            g.FillEllipse(brush, Bounds);
            using var innerBrush = new SolidBrush(Color.FromArgb(255, 200, 100, 255));
            g.FillEllipse(innerBrush, Position.X + 4, Position.Y + 4, 12, 12);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player || other is Level.Tile || other is Level.Obstacle)
                IsActive = false;
        }
    }
}
