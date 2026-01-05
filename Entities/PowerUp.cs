using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// PowerUp collectible entity.
    /// </summary>
    public class PowerUp : GameObject, ICollidable
    {
        public PowerUpType Type { get; set; }
        private float _bobOffset;

        public PowerUp(float x, float y, PowerUpType type = PowerUpType.Health)
        {
            Position = new PointF(x, y);
            Size = new SizeF(32, 32);
            Type = type;
        }

        public override void Update(GameTime gameTime)
        {
            // Bobbing animation
            _bobOffset += 0.1f;
            Position = new PointF(Position.X, Position.Y + (float)Math.Sin(_bobOffset) * 0.5f);
        }

        public override void Draw(Graphics g)
        {
            var color = Type == PowerUpType.Health ? Brushes.LimeGreen : Brushes.Gold;
            g.FillEllipse(color, Bounds);
            g.DrawEllipse(Pens.White, Bounds);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
                IsActive = false;
        }
    }

    public enum PowerUpType
    {
        Health,
        Score
    }
}
