using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Movements
{
    /// <summary>
    /// Aggressive AI movement for Level 3 enemies.
    /// Chases player in all directions: horizontal, vertical, diagonal, zig-zag.
    /// </summary>
    public class AggressiveAIMovement : IMovement
    {
        private static readonly Random _random = new();
        
        private float _speed;
        private float _baseSpeed;
        private float _directionX;
        private float _directionY;
        private float _patternTimer;
        private float _zigzagTimer;
        private readonly float _minX;
        private readonly float _maxX;
        private readonly float _minY;
        private readonly float _maxY;
        private MovementPattern _currentPattern;
        private PointF? _targetPosition;
        private bool _isChasing;
        
        public bool FacingRight => _directionX >= 0;
        public bool IsIdle => false; // Never idle - always aggressive

        private enum MovementPattern
        {
            Chase,
            ZigZag,
            Diagonal,
            Surround,
            Vertical
        }

        public AggressiveAIMovement(float minX, float maxX, float speed = 3f, float minY = 300, float maxY = 500)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
            _baseSpeed = speed;
            _speed = speed;
            _directionX = _random.Next(2) == 0 ? 1 : -1;
            _directionY = 0;
            _patternTimer = GetRandomPatternTime();
            _currentPattern = MovementPattern.Chase;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            
            // Update pattern timer
            _patternTimer -= dt;
            if (_patternTimer <= 0)
            {
                SwitchPattern();
                _patternTimer = GetRandomPatternTime();
            }

            // Execute current movement pattern
            switch (_currentPattern)
            {
                case MovementPattern.ZigZag:
                    ExecuteZigZag(obj, dt);
                    break;
                case MovementPattern.Diagonal:
                    ExecuteDiagonal(obj, dt);
                    break;
                case MovementPattern.Surround:
                    ExecuteSurround(obj, dt);
                    break;
                case MovementPattern.Vertical:
                    ExecuteVertical(obj, dt);
                    break;
                default:
                    ExecuteChase(obj, dt);
                    break;
            }

            // Apply movement with boundary checks
            ApplyMovement(obj);
        }

        private void SwitchPattern()
        {
            // Randomly select a new pattern
            var patterns = Enum.GetValues<MovementPattern>();
            _currentPattern = patterns[_random.Next(patterns.Length)];
            
            // Reset zig-zag timer for new pattern
            _zigzagTimer = 0;
            
            // Randomize speed variation
            _speed = _baseSpeed * (0.8f + (float)_random.NextDouble() * 0.6f);
        }

        private void ExecuteChase(GameObject obj, float dt)
        {
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                float dy = _targetPosition.Value.Y - obj.Position.Y;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                
                if (dist > 10)
                {
                    _directionX = dx / dist;
                    _directionY = dy / dist * 0.3f; // Reduced vertical chase
                }
            }
        }

        private void ExecuteZigZag(GameObject obj, float dt)
        {
            _zigzagTimer += dt;
            
            // Zig-zag horizontally while moving toward target
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                _directionX = dx > 0 ? 1 : -1;
            }
            
            // Oscillate vertically
            _directionY = (float)Math.Sin(_zigzagTimer * 8) * 0.5f;
        }

        private void ExecuteDiagonal(GameObject obj, float dt)
        {
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                float dy = _targetPosition.Value.Y - obj.Position.Y;
                
                // Move diagonally toward player
                _directionX = dx > 0 ? 0.7f : -0.7f;
                _directionY = dy > 0 ? 0.3f : -0.3f;
            }
        }

        private void ExecuteSurround(GameObject obj, float dt)
        {
            _zigzagTimer += dt;
            
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                float dist = Math.Abs(dx);
                
                // Circle around the player
                if (dist < 150)
                {
                    // Move away slightly while circling
                    _directionX = dx > 0 ? -0.5f : 0.5f;
                    _directionY = (float)Math.Sin(_zigzagTimer * 4) * 0.4f;
                }
                else
                {
                    // Approach
                    _directionX = dx > 0 ? 1 : -1;
                    _directionY = (float)Math.Sin(_zigzagTimer * 6) * 0.3f;
                }
            }
        }

        private void ExecuteVertical(GameObject obj, float dt)
        {
            _zigzagTimer += dt;
            
            // Strong vertical movement while slowly approaching
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                _directionX = (dx > 0 ? 0.3f : -0.3f);
            }
            
            // Oscillate vertically more aggressively
            _directionY = (float)Math.Sin(_zigzagTimer * 5) * 0.6f;
        }

        private void ApplyMovement(GameObject obj)
        {
            float newX = obj.Position.X + (_directionX * _speed);
            float newY = obj.Position.Y + (_directionY * _speed);
            
            // Horizontal boundary checks
            if (newX < _minX)
            {
                newX = _minX;
                _directionX = Math.Abs(_directionX);
            }
            else if (newX + obj.Size.Width > _maxX)
            {
                newX = _maxX - obj.Size.Width;
                _directionX = -Math.Abs(_directionX);
            }
            
            // Vertical boundary checks
            if (newY < _minY)
            {
                newY = _minY;
                _directionY = Math.Abs(_directionY);
            }
            else if (newY > _maxY)
            {
                newY = _maxY;
                _directionY = -Math.Abs(_directionY);
            }

            obj.Position = new PointF(newX, newY);
        }

        private float GetRandomPatternTime()
        {
            return 1.5f + (float)_random.NextDouble() * 2f; // 1.5-3.5 seconds per pattern
        }

        /// <summary>
        /// Set target position (player position) for chasing
        /// </summary>
        public void SetTarget(PointF target)
        {
            _targetPosition = target;
            _isChasing = true;
        }

        public void SetDirection(float dirX)
        {
            _directionX = dirX > 0 ? 1 : -1;
        }
    }
}
