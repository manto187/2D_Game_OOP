using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// Bullet projectile entity.
    /// </summary>
    public class Bullet : GameObject, ICollidable
    {
        public float Speed { get; set; } = 15f;
        public int Damage { get; set; } = 10;
        public bool MovingRight { get; set; } = true;

        public Bullet(float x, float y, bool movingRight)
        {
            Position = new PointF(x, y);
            Size = new SizeF(10, 5);
            MovingRight = movingRight;
            Velocity = new PointF(movingRight ? Speed : -Speed, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Position = new PointF(Position.X + Velocity.X, Position.Y);
        }

        public override void Draw(Graphics g)
        {
            // Draw bullet with glow effect
            using var glowBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 100));
            g.FillEllipse(glowBrush, Bounds.X - 2, Bounds.Y - 2, Bounds.Width + 4, Bounds.Height + 4);
            g.FillEllipse(Brushes.Yellow, Bounds);
            g.FillEllipse(Brushes.White, Bounds.X + 2, Bounds.Y + 1, Bounds.Width - 4, Bounds.Height - 2);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy || other is Level.Tile || other is Level.Obstacle)
                IsActive = false;
        }
    }
}
