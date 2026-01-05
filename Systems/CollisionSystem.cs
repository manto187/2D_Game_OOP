using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Level;
using FirstDesktopApp.Movements;

namespace FirstDesktopApp.Systems
{
    /// <summary>
    /// Handles collision detection and resolution with proper AABB collision.
    /// </summary>
    public class CollisionSystem
    {
        public void Check(List<GameObject> objects)
        {
            var collidables = objects.Where(o => o is ICollidable && o.IsActive).ToList();
            var player = collidables.OfType<Player>().FirstOrDefault();
            var solids = collidables.Where(o => o is Tile || o is Obstacle).ToList();
            
            // Resolve player collisions with all solids
            if (player != null)
            {
                // Reset grounded - will be set true if we land on something
                if (player.Movement is KeyboardMovement km)
                    km.SetGrounded(false);
                
                ResolvePlayerCollisions(player, solids);
            }

            // Check other collisions (enemies, bullets, powerups, etc.)
            for (int i = 0; i < collidables.Count; i++)
            {
                for (int j = i + 1; j < collidables.Count; j++)
                {
                    var a = collidables[i];
                    var b = collidables[j];

                    // Skip tile-tile and obstacle-obstacle collisions
                    if ((a is Tile || a is Obstacle) && (b is Tile || b is Obstacle))
                        continue;
                    
                    // Skip player-solid (already handled above)
                    if ((a is Player && (b is Tile || b is Obstacle)) || 
                        (b is Player && (a is Tile || a is Obstacle)))
                        continue;

                    if (!a.Bounds.IntersectsWith(b.Bounds))
                        continue;

                    ((ICollidable)a).OnCollision(b);
                    ((ICollidable)b).OnCollision(a);
                }
            }
        }

        private void ResolvePlayerCollisions(Player player, List<GameObject> solids)
        {
            // Sort solids by distance to player for better resolution order
            var sortedSolids = solids
                .Where(s => player.Bounds.IntersectsWith(s.Bounds))
                .OrderBy(s => GetDistance(player, s))
                .ToList();

            foreach (var solid in sortedSolids)
            {
                if (!player.Bounds.IntersectsWith(solid.Bounds))
                    continue;

                ResolveCollision(player, solid);
            }
            
            // Second pass to catch any remaining collisions
            foreach (var solid in solids)
            {
                if (!player.Bounds.IntersectsWith(solid.Bounds))
                    continue;

                ResolveCollision(player, solid);
            }
        }

        private float GetDistance(GameObject a, GameObject b)
        {
            float dx = (a.Position.X + a.Size.Width / 2) - (b.Position.X + b.Size.Width / 2);
            float dy = (a.Position.Y + a.Size.Height / 2) - (b.Position.Y + b.Size.Height / 2);
            return dx * dx + dy * dy;
        }

        private void ResolveCollision(Player player, GameObject solid)
        {
            // Get player bounds
            float playerLeft = player.Position.X;
            float playerRight = player.Position.X + player.Size.Width;
            float playerTop = player.Position.Y;
            float playerBottom = player.Position.Y + player.Size.Height;

            // Get solid bounds
            float solidLeft = solid.Position.X;
            float solidRight = solid.Position.X + solid.Size.Width;
            float solidTop = solid.Position.Y;
            float solidBottom = solid.Position.Y + solid.Size.Height;

            // Calculate penetration on each axis
            float overlapLeft = playerRight - solidLeft;   // Player's right into solid's left
            float overlapRight = solidRight - playerLeft;  // Solid's right into player's left
            float overlapTop = playerBottom - solidTop;    // Player's bottom into solid's top
            float overlapBottom = solidBottom - playerTop; // Solid's bottom into player's top

            // Find minimum overlap
            float minOverlapX = (overlapLeft < overlapRight) ? overlapLeft : overlapRight;
            float minOverlapY = (overlapTop < overlapBottom) ? overlapTop : overlapBottom;

            // Determine push direction
            bool pushLeft = overlapLeft < overlapRight;
            bool pushUp = overlapTop < overlapBottom;

            // Use velocity to help determine collision direction
            bool movingDown = player.Velocity.Y > 0;
            bool movingUp = player.Velocity.Y < 0;
            bool movingRight = player.Velocity.X > 0;
            bool movingLeft = player.Velocity.X < 0;

            // Bias toward vertical resolution when landing (player moving down onto platform)
            if (movingDown && overlapTop < 30 && overlapTop < minOverlapX)
            {
                // Landing on top
                player.Position = new PointF(player.Position.X, solidTop - player.Size.Height);
                player.Velocity = new PointF(player.Velocity.X, 0);
                if (player.Movement is KeyboardMovement km)
                    km.SetGrounded(true);
                return;
            }

            // Hitting ceiling
            if (movingUp && overlapBottom < 20 && overlapBottom < minOverlapX)
            {
                player.Position = new PointF(player.Position.X, solidBottom);
                player.Velocity = new PointF(player.Velocity.X, 0);
                return;
            }

            // Resolve based on smallest penetration
            if (minOverlapX < minOverlapY)
            {
                // Horizontal collision
                if (pushLeft)
                    player.Position = new PointF(solidLeft - player.Size.Width, player.Position.Y);
                else
                    player.Position = new PointF(solidRight, player.Position.Y);
            }
            else
            {
                // Vertical collision
                if (pushUp)
                {
                    // Landing on top
                    player.Position = new PointF(player.Position.X, solidTop - player.Size.Height);
                    player.Velocity = new PointF(player.Velocity.X, 0);
                    if (player.Movement is KeyboardMovement km)
                        km.SetGrounded(true);
                }
                else
                {
                    // Hitting from below
                    player.Position = new PointF(player.Position.X, solidBottom);
                    player.Velocity = new PointF(player.Velocity.X, 0);
                }
            }
        }
    }
}
