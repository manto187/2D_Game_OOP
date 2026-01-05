using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Level;

namespace FirstDesktopApp.Movements
{
    /// <summary>
    /// Random AI movement for enemies - moves in random directions, 
    /// changes direction randomly, idles, and respects tile boundaries.
    /// </summary>
    public class RandomAIMovement : IMovement
    {
        private static readonly Random _random = new();
        
        private float _speed;
        private float _baseSpeed;
        private float _directionX;
        private float _directionChangeTimer;
        private float _idleTimer;
        private bool _isIdle;
        private readonly float _minX;
        private readonly float _maxX;
        private bool _isChasing;
        private float _chaseTimer;
        
        public bool FacingRight => _directionX > 0;
        public bool IsIdle => _isIdle;

        public RandomAIMovement(float minX, float maxX, float speed = 2f)
        {
            _minX = minX;
            _maxX = maxX;
            _baseSpeed = speed;
            _speed = speed;
            _directionX = _random.Next(2) == 0 ? 1 : -1;
            _directionChangeTimer = GetRandomDirectionTime();
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            
            // Update chase timer
            if (_isChasing)
            {
                _chaseTimer -= dt;
                if (_chaseTimer <= 0)
                {
                    _isChasing = false;
                    _speed = _baseSpeed;
                }
            }
            
            // Handle idle state
            if (_isIdle)
            {
                _idleTimer -= dt;
                if (_idleTimer <= 0)
                {
                    _isIdle = false;
                    // Random direction after idle
                    _directionX = _random.Next(2) == 0 ? 1 : -1;
                    _directionChangeTimer = GetRandomDirectionTime();
                }
                return;
            }

            // Update direction change timer (only when not chasing)
            if (!_isChasing)
            {
                _directionChangeTimer -= dt;
                if (_directionChangeTimer <= 0)
                {
                    // Randomly decide: change direction, stop, or continue
                    int action = _random.Next(100);
                    
                    if (action < 15)
                    {
                        // 15% chance to stop and idle
                        _isIdle = true;
                        _idleTimer = 0.3f + (float)_random.NextDouble() * 1.5f; // 0.3-1.8 seconds
                    }
                    else if (action < 50)
                    {
                        // 35% chance to reverse direction
                        _directionX = -_directionX;
                    }
                    else if (action < 70)
                    {
                        // 20% chance to speed up briefly
                        _speed = _baseSpeed * 1.5f;
                    }
                    else
                    {
                        // 30% chance to continue same direction at normal speed
                        _speed = _baseSpeed;
                    }
                    
                    _directionChangeTimer = GetRandomDirectionTime();
                }
            }

            // Move in current direction
            float moveAmount = _directionX * _speed;
            float newX = obj.Position.X + moveAmount;
            
            // Boundary checks - reverse if hitting bounds
            if (newX <= _minX)
            {
                newX = _minX;
                _directionX = 1;
                _directionChangeTimer = GetRandomDirectionTime();
            }
            else if (newX + obj.Size.Width >= _maxX)
            {
                newX = _maxX - obj.Size.Width;
                _directionX = -1;
                _directionChangeTimer = GetRandomDirectionTime();
            }

            obj.Position = new PointF(newX, obj.Position.Y);
        }

        private float GetRandomDirectionTime()
        {
            return 0.8f + (float)_random.NextDouble() * 2.5f; // 0.8-3.3 seconds
        }

        /// <summary>
        /// Called when enemy detects player - makes enemy chase in that direction
        /// </summary>
        public void SetDirection(float dirX)
        {
            _directionX = dirX > 0 ? 1 : -1;
            _isIdle = false;
            _isChasing = true;
            _chaseTimer = 2f; // Chase for 2 seconds
            _speed = _baseSpeed * 1.3f; // Speed up when chasing
        }
    }
}
