using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// Base class for all game objects.
    /// Implements IMovable and ICollidable for game systems.
    /// Open/Closed: Can be extended without modification.
    /// </summary>
    public class GameObject : IMovable, ICollidable, IPhysicsObject
    {
        public PointF Position { get; set; }
        public SizeF Size { get; set; }
        public PointF Velocity { get; set; } = PointF.Empty;
        public bool IsActive { get; set; } = true;
        public bool HasPhysics { get; set; } = false;
        public float? CustomGravity { get; set; } = null;
        public bool IsRigidBody { get; set; } = false;
        public Image? Sprite { get; set; } = null;
        public RectangleF Bounds => new RectangleF(Position, Size);

        public virtual void Update(GameTime gameTime)
        {
            Position = new PointF(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public virtual void Draw(Graphics graphics)
        {
            if (Sprite != null)
                graphics.DrawImage(Sprite, Bounds);
            else
                using (Brush brush = new SolidBrush(Color.Gray))
                    graphics.FillRectangle(brush, Bounds);
        }

        public virtual void OnCollision(GameObject other)
        {
            // Default: no reaction
        }
    }
}

