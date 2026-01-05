using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Movements;
using FirstDesktopApp.Rendering;

namespace FirstDesktopApp.Entities
{
    public enum EnemyType { Wraith01, Wraith02, Wraith03 }
    public enum EnemyState { Idle, Walking, Attacking, Hurt, Dying }

    /// <summary>
    /// Enemy entity with AI, random movement, attacks, and animations.
    /// </summary>
    public class Enemy : GameObject, ICollidable
    {
        private static readonly Random _random = new();
        
        public IMovement? Movement { get; set; }
        public AnimatedSprite? AnimatedSprite { get; set; }
        public EnemyType Type { get; set; }
        public EnemyState State { get; private set; } = EnemyState.Walking;
        public int Health { get; set; } = 30;
        public int ScoreValue { get; set; } = 100;
        public float AttackRange { get; set; } = 300f;
        public float AttackCooldown { get; set; } = 2f;
        
        public bool IsDying => State == EnemyState.Dying;
        public bool FacingRight { get; private set; }

        private float _stateTimer;
        private float _attackTimer;
        private float _animTimer;
        private int _currentFrame;
        private Image[]? _currentFrames;
        private float _spellCooldown;
        private float _aggroRange = 400f;
        
        // Events
        public event Action<Enemy>? OnDeath;
        public event Action<EnemyProjectile>? OnAttack;

        // Cached animation frames for performance
        private Dictionary<string, Image[]> _animationFrames = new();

        public Enemy(EnemyType type = EnemyType.Wraith01)
        {
            Type = type;
            Size = new SizeF(90, 90);
            
            ScoreValue = type switch
            {
                EnemyType.Wraith02 => 150,
                EnemyType.Wraith03 => 200,
                _ => 100
            };

            Health = type switch
            {
                EnemyType.Wraith02 => 40,
                EnemyType.Wraith03 => 50,
                _ => 30
            };

            // Faster attack cooldowns for more aggressive enemies
            AttackCooldown = type switch
            {
                EnemyType.Wraith03 => 0.8f,  // Very aggressive
                EnemyType.Wraith02 => 1.2f,  // Aggressive
                _ => 1.8f                     // Normal
            };
            
            // Larger aggro and attack ranges
            _aggroRange = type switch
            {
                EnemyType.Wraith03 => 600f,
                EnemyType.Wraith02 => 500f,
                _ => 400f
            };
            
            AttackRange = type switch
            {
                EnemyType.Wraith03 => 400f,
                EnemyType.Wraith02 => 350f,
                _ => 300f
            };
        }

        public void SetAnimationFrames(string name, Image[] frames)
        {
            _animationFrames[name] = frames;
            if (_currentFrames == null && name == "Walking")
            {
                _currentFrames = frames;
            }
        }

        public override void Update(GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            _attackTimer -= dt;
            _spellCooldown -= dt;

            switch (State)
            {
                case EnemyState.Dying:
                    UpdateDying(dt);
                    break;
                case EnemyState.Hurt:
                    UpdateHurt(dt);
                    break;
                case EnemyState.Attacking:
                    UpdateAttacking(dt);
                    break;
                case EnemyState.Idle:
                    UpdateIdle(dt);
                    break;
                default:
                    UpdateWalking(dt);
                    break;
            }

            UpdateAnimation(dt);
        }

        private void UpdateWalking(float dt)
        {
            Movement?.Move(this, new GameTime { DeltaTime = dt * 60f });
            
            // Update facing direction based on movement type
            if (Movement is PatrolMovement patrol)
                FacingRight = patrol.FacingRight;
            else if (Movement is RandomAIMovement randomAI)
            {
                FacingRight = randomAI.FacingRight;
                
                // Check if enemy should idle
                if (randomAI.IsIdle && State != EnemyState.Idle)
                {
                    State = EnemyState.Idle;
                    SetAnimation("Idle");
                    return;
                }
            }
            else if (Movement is AggressiveAIMovement aggressiveAI)
            {
                FacingRight = aggressiveAI.FacingRight;
                // Aggressive AI never idles
            }

            SetAnimation("Walking");
        }
        
        private void UpdateIdle(float dt)
        {
            // Check if movement wants to resume
            if (Movement is RandomAIMovement randomAI && !randomAI.IsIdle)
            {
                State = EnemyState.Walking;
                SetAnimation("Walking");
            }
        }

        private void UpdateAttacking(float dt)
        {
            _stateTimer -= dt;
            if (_stateTimer <= 0)
                State = EnemyState.Walking;
        }

        private void UpdateHurt(float dt)
        {
            _stateTimer -= dt;
            if (_stateTimer <= 0)
                State = EnemyState.Walking;
        }

        private void UpdateDying(float dt)
        {
            _stateTimer -= dt;
            if (_stateTimer <= 0)
                IsActive = false;
        }

        private void UpdateAnimation(float dt)
        {
            if (_currentFrames == null || _currentFrames.Length == 0)
                return;

            _animTimer += dt;
            float frameTime = State == EnemyState.Dying ? 0.06f : 0.08f;
            
            if (_animTimer >= frameTime)
            {
                _animTimer = 0;
                _currentFrame++;
                
                if (_currentFrame >= _currentFrames.Length)
                {
                    _currentFrame = State == EnemyState.Dying ? _currentFrames.Length - 1 : 0;
                }
            }
        }

        private void SetAnimation(string name)
        {
            if (_animationFrames.TryGetValue(name, out var frames) && frames != _currentFrames)
            {
                _currentFrames = frames;
                _currentFrame = 0;
                _animTimer = 0;
            }
        }

        public void TryAttack(Player? player)
        {
            if (player == null || player.IsDead || State == EnemyState.Dying || State == EnemyState.Hurt)
                return;

            float distX = player.Position.X - Position.X;
            float distY = Math.Abs(player.Position.Y - Position.Y);
            float dist = Math.Abs(distX);
            
            // Face the player when in aggro range
            if (dist < _aggroRange && distY < 200)
            {
                FacingRight = distX > 0;
                
                // Update movement AI with player position
                if (Movement is RandomAIMovement randomAI)
                {
                    randomAI.SetDirection(distX);
                }
                else if (Movement is AggressiveAIMovement aggressiveAI)
                {
                    // Set target for aggressive AI to chase
                    aggressiveAI.SetTarget(player.Position);
                }
            }

            // Attack when in range and cooldown is ready
            if (dist < AttackRange && distY < 150 && _attackTimer <= 0)
            {
                State = EnemyState.Attacking;
                _stateTimer = 0.6f;
                _attackTimer = AttackCooldown;
                SetAnimation("Attacking");

                // Fire projectile toward player
                FacingRight = distX > 0;
                float projX = FacingRight ? Position.X + Size.Width : Position.X - 20;
                var projectile = new EnemyProjectile(projX, Position.Y + Size.Height / 2 - 10, FacingRight);
                OnAttack?.Invoke(projectile);
            }
        }

        public override void Draw(Graphics g)
        {
            if (_currentFrames == null || _currentFrames.Length == 0 || _currentFrame >= _currentFrames.Length)
            {
                g.FillRectangle(Brushes.Purple, Bounds);
                return;
            }

            var frame = _currentFrames[_currentFrame];
            if (FacingRight)
            {
                g.DrawImage(frame, Bounds);
            }
            else
            {
                // Flip horizontally
                g.TranslateTransform(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);
                g.ScaleTransform(-1, 1);
                g.DrawImage(frame, -Size.Width / 2, -Size.Height / 2, Size.Width, Size.Height);
                g.ResetTransform();
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Bullet && State != EnemyState.Dying)
            {
                Health -= 10;
                
                if (Health <= 0)
                {
                    State = EnemyState.Dying;
                    _stateTimer = 1.0f;
                    SetAnimation("Dying");
                    OnDeath?.Invoke(this);
                }
                else
                {
                    State = EnemyState.Hurt;
                    _stateTimer = 0.3f;
                    SetAnimation("Hurt");
                }
            }
        }
    }
}

