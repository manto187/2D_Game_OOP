using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// PowerUp collectible entity (Health Packs).
    /// </summary>
    public class PowerUp : GameObject, ICollidable
    {
        public PowerUpType Type { get; set; }
        public int HealAmount { get; set; } = 25;
        private float _bobOffset;
        private float _baseY;

        public PowerUp(float x, float y, PowerUpType type = PowerUpType.Health, Image? sprite = null)
        {
            Position = new PointF(x, y);
            _baseY = y;
            Size = new SizeF(48, 48);
            Type = type;
            Sprite = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            // Bobbing animation
            _bobOffset += 0.08f;
            Position = new PointF(Position.X, _baseY + (float)Math.Sin(_bobOffset) * 4f);
        }

        public override void Draw(Graphics g)
        {
            if (Sprite != null)
            {
                g.DrawImage(Sprite, Bounds);
            }
            else
            {
                // Fallback drawing
                var color = Type == PowerUpType.Health ? Brushes.LimeGreen : Brushes.Gold;
                g.FillEllipse(color, Bounds);
                g.DrawEllipse(Pens.White, Bounds);
                
                // Draw cross for health
                if (Type == PowerUpType.Health)
                {
                    using var pen = new Pen(Color.White, 3);
                    float cx = Position.X + Size.Width / 2;
                    float cy = Position.Y + Size.Height / 2;
                    g.DrawLine(pen, cx - 8, cy, cx + 8, cy);
                    g.DrawLine(pen, cx, cy - 8, cx, cy + 8);
                }
            }
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
