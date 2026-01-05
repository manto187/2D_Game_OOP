using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Represents a single tile in the game world.
    /// Implements ICollidable for collision detection.
    /// </summary>
    public class Tile : GameObject, ICollidable
    {
        public TileType Type { get; }

        public Tile(float x, float y, float size, TileType type, Image? sprite = null)
        {
            Position = new PointF(x, y);
            Size = new SizeF(size, size);
            Type = type;
            Sprite = sprite;
            IsRigidBody = type == TileType.Solid;
        }

        public override void OnCollision(GameObject other)
        {
            // Tiles don't react to collisions, they just block
        }
    }

    public enum TileType
    {
        Empty,
        Solid,
        Platform,
        Hazard
    }
}
