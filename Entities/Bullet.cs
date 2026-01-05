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
            g.FillEllipse(Brushes.Yellow, Bounds);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy || other is Level.Tile || other is Level.Obstacle)
                IsActive = false;
        }
    }
}
