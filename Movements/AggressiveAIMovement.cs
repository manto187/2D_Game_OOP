using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Movements
{
    /// <summary>
    /// Aggressive AI movement for enemies.
    /// Moves in ALL directions: horizontal, vertical, diagonal, zig-zag patterns.
    /// Chases player aggressively.
    /// </summary>
    public class AggressiveAIMovement : IMovement
    {
        private static readonly Random _random = new();
        
        private float _speed;
        private float _baseSpeed;
        private float _directionX;
        private float _directionY;
        private float _patternTimer;
        private float _moveTimer;
        private readonly float _minX;
        private readonly float _maxX;
        private readonly float _minY;
        private readonly float _maxY;
        private MovementPattern _currentPattern;
        private PointF? _targetPosition;
        private float _zigzagPhase;
        
        public bool FacingRight => _directionX >= 0;
        public bool IsIdle => false;

        private enum MovementPattern
        {
            DirectChase,
            ZigZagHorizontal,
            ZigZagVertical,
            DiagonalTopRight,
            DiagonalTopLeft,
            DiagonalBottomRight,
            DiagonalBottomLeft,
            CircleAround,
            VerticalBounce,
            RandomWander
        }

        public AggressiveAIMovement(float minX, float maxX, float speed = 3f, float minY = 280, float maxY = 450)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
            _baseSpeed = speed;
            _speed = speed;
            _directionX = _random.Next(2) == 0 ? 1 : -1;
            _directionY = _random.Next(2) == 0 ? 0.5f : -0.5f;
            _patternTimer = GetRandomPatternTime();
            _currentPattern = GetRandomPattern();
            _zigzagPhase = (float)_random.NextDouble() * 6.28f;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            _moveTimer += dt;
            _zigzagPhase += dt * 5f;
            
            // Switch patterns periodically
            _patternTimer -= dt;
            if (_patternTimer <= 0)
            {
                _currentPattern = GetRandomPattern();
                _patternTimer = GetRandomPatternTime();
                _speed = _baseSpeed * (0.9f + (float)_random.NextDouble() * 0.4f);
            }

            // Execute current movement pattern
            ExecutePattern(obj, dt);

            // Apply movement with boundary checks
            ApplyMovement(obj);
        }

        private MovementPattern GetRandomPattern()
        {
            var patterns = Enum.GetValues<MovementPattern>();
            return patterns[_random.Next(patterns.Length)];
        }

        private void ExecutePattern(GameObject obj, float dt)
        {
            switch (_currentPattern)
            {
                case MovementPattern.DirectChase:
                    ExecuteDirectChase(obj);
                    break;
                case MovementPattern.ZigZagHorizontal:
                    ExecuteZigZagHorizontal(obj);
                    break;
                case MovementPattern.ZigZagVertical:
                    ExecuteZigZagVertical(obj);
                    break;
                case MovementPattern.DiagonalTopRight:
                    _directionX = 0.7f;
                    _directionY = -0.7f;
                    break;
                case MovementPattern.DiagonalTopLeft:
                    _directionX = -0.7f;
                    _directionY = -0.7f;
                    break;
                case MovementPattern.DiagonalBottomRight:
                    _directionX = 0.7f;
                    _directionY = 0.7f;
                    break;
                case MovementPattern.DiagonalBottomLeft:
                    _directionX = -0.7f;
                    _directionY = 0.7f;
                    break;
                case MovementPattern.CircleAround:
                    ExecuteCircleAround(obj);
                    break;
                case MovementPattern.VerticalBounce:
                    ExecuteVerticalBounce(obj);
                    break;
                case MovementPattern.RandomWander:
                    ExecuteRandomWander(obj);
                    break;
            }
        }

        private void ExecuteDirectChase(GameObject obj)
        {
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                float dy = _targetPosition.Value.Y - obj.Position.Y;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                
                if (dist > 5)
                {
                    _directionX = dx / dist;
                    _directionY = dy / dist;
                }
            }
            else
            {
                // Wander if no target
                _directionY = (float)Math.Sin(_zigzagPhase) * 0.5f;
            }
        }

        private void ExecuteZigZagHorizontal(GameObject obj)
        {
            // Move horizontally toward target while oscillating vertically
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                _directionX = dx > 0 ? 1f : -1f;
            }
            _directionY = (float)Math.Sin(_zigzagPhase) * 0.8f;
        }

        private void ExecuteZigZagVertical(GameObject obj)
        {
            // Move vertically while oscillating horizontally
            if (_targetPosition.HasValue)
            {
                float dy = _targetPosition.Value.Y - obj.Position.Y;
                _directionY = dy > 0 ? 0.6f : -0.6f;
            }
            _directionX = (float)Math.Sin(_zigzagPhase) * 0.8f;
        }

        private void ExecuteCircleAround(GameObject obj)
        {
            // Circular motion around current position
            _directionX = (float)Math.Cos(_zigzagPhase) * 0.8f;
            _directionY = (float)Math.Sin(_zigzagPhase) * 0.6f;
            
            // Slowly drift toward target
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                float dy = _targetPosition.Value.Y - obj.Position.Y;
                _directionX += dx > 0 ? 0.2f : -0.2f;
                _directionY += dy > 0 ? 0.1f : -0.1f;
            }
        }

        private void ExecuteVerticalBounce(GameObject obj)
        {
            // Strong vertical movement
            _directionY = (float)Math.Sin(_zigzagPhase * 1.5f) * 1f;
            
            // Slow horizontal drift toward target
            if (_targetPosition.HasValue)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                _directionX = dx > 0 ? 0.4f : -0.4f;
            }
        }

        private void ExecuteRandomWander(GameObject obj)
        {
            // Change direction randomly
            if (_random.Next(100) < 5)
            {
                _directionX = (float)(_random.NextDouble() * 2 - 1) * 1f;
                _directionY = (float)(_random.NextDouble() * 2 - 1) * 0.6f;
            }
            
            // Occasionally move toward target
            if (_targetPosition.HasValue && _random.Next(100) < 20)
            {
                float dx = _targetPosition.Value.X - obj.Position.X;
                float dy = _targetPosition.Value.Y - obj.Position.Y;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                if (dist > 5)
                {
                    _directionX = (dx / dist) * 0.8f;
                    _directionY = (dy / dist) * 0.5f;
                }
            }
        }

        private void ApplyMovement(GameObject obj)
        {
            float newX = obj.Position.X + (_directionX * _speed);
            float newY = obj.Position.Y + (_directionY * _speed);
            
            // Horizontal boundary checks - bounce off walls
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
            
            // Vertical boundary checks - bounce off ceiling/floor
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
            return 1f + (float)_random.NextDouble() * 2f;
        }

        public void SetTarget(PointF target)
        {
            _targetPosition = target;
        }

        public void SetDirection(float dirX)
        {
            _directionX = dirX > 0 ? 1 : -1;
        }
        
        public void SetSpeed(float speed)
        {
            _baseSpeed = speed;
            _speed = speed;
        }
    }
}
