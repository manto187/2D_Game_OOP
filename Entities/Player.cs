using FirstDesktopApp.Core;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Movements;
using FirstDesktopApp.Rendering;
using EZInput;

namespace FirstDesktopApp.Entities
{
    /// <summary>
    /// Player entity with animated sprites, platformer controls, and shooting.
    /// </summary>
    public class Player : GameObject, ICollidable, IPhysicsObject
    {
        public IMovement? Movement { get; set; }
        public AnimatedSprite? AnimatedSprite { get; set; }
        public int Health { get; set; } = 100;
        public int Score { get; set; } = 0;
        public bool IsDead => Health <= 0;

        private float _invincibilityTimer;
        private float _shootCooldown;
        private bool _isShooting;
        private float _shootAnimTimer;
        private const float InvincibilityDuration = 1.5f;
        private const float ShootCooldown = 0.3f;

        // Event for spawning bullets
        public event Action<Bullet>? OnShoot;

        public Player()
        {
            HasPhysics = true;
            Size = new SizeF(80, 80);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;

            if (IsDead)
            {
                AnimatedSprite?.Play("Dead");
                AnimatedSprite?.Update(dt);
                return;
            }

            Movement?.Move(this, gameTime);

            // Update timers
            if (_invincibilityTimer > 0)
                _invincibilityTimer -= dt;
            if (_shootCooldown > 0)
                _shootCooldown -= dt;

            // Handle shooting
            HandleShooting(dt);

            // Update animation
            UpdateAnimation(dt);
        }

        private void HandleShooting(float dt)
        {
            if (_isShooting)
            {
                _shootAnimTimer -= dt;
                if (_shootAnimTimer <= 0)
                    _isShooting = false;
            }

            // Shoot with X or Ctrl
            if ((Keyboard.IsKeyPressed(Key.X) || Keyboard.IsKeyPressed(Key.Control)) && _shootCooldown <= 0)
            {
                _shootCooldown = ShootCooldown;
                _isShooting = true;
                _shootAnimTimer = 0.2f;

                var keyboard = Movement as KeyboardMovement;
                bool facingRight = keyboard?.FacingRight ?? true;
                
                float bulletX = facingRight ? Position.X + Size.Width : Position.X - 10;
                float bulletY = Position.Y + Size.Height / 2 - 5;
                
                var bullet = new Bullet(bulletX, bulletY, facingRight);
                OnShoot?.Invoke(bullet);
            }
        }

        private void UpdateAnimation(float dt)
        {
            if (AnimatedSprite == null) return;

            var keyboard = Movement as KeyboardMovement;
            if (keyboard != null)
            {
                AnimatedSprite.IsFlipped = !keyboard.FacingRight;

                if (_isShooting)
                    AnimatedSprite.Play("Shoot");
                else if (!keyboard.IsGrounded)
                    AnimatedSprite.Play("Jump");
                else if (Keyboard.IsKeyPressed(Key.LeftArrow) || Keyboard.IsKeyPressed(Key.RightArrow) ||
                         Keyboard.IsKeyPressed(Key.A) || Keyboard.IsKeyPressed(Key.D))
                    AnimatedSprite.Play("Run");
                else
                    AnimatedSprite.Play("Idle");
            }

            AnimatedSprite.Update(dt);
        }

        public override void Draw(Graphics g)
        {
            if (_invincibilityTimer > 0 && ((int)(_invincibilityTimer * 10) % 2 == 0))
                return;

            var frame = AnimatedSprite?.GetCurrentFrame();
            if (frame != null)
                g.DrawImage(frame, Bounds);
            else
                base.Draw(g);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy enemy && !enemy.IsDying && _invincibilityTimer <= 0)
            {
                TakeDamage(20);
            }

            if (other is PowerUp)
            {
                Health = Math.Min(100, Health + 20);
                Score += 100;
            }
        }

        public void TakeDamage(int damage)
        {
            if (_invincibilityTimer > 0 || IsDead) return;
            Health -= damage;
            _invincibilityTimer = InvincibilityDuration;
        }
    }
}

