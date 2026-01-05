using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Represents obstacles/hurdles in the game (crates, stones, etc.)
    /// </summary>
    public class Obstacle : GameObject, ICollidable
    {
        public bool IsDangerous { get; set; }

        public Obstacle(float x, float y, float width, float height, Image sprite, bool isDangerous = false)
        {
            Position = new PointF(x, y);
            Size = new SizeF(width, height);
            Sprite = sprite;
            IsDangerous = isDangerous;
            IsRigidBody = true;
        }

        public override void OnCollision(GameObject other)
        {
            // Obstacles are static, no reaction needed
        }
    }
}
