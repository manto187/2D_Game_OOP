using EZInput;
using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;
using FirstDesktopApp.Systems;

namespace FirstDesktopApp.Movements
{
    /// <summary>
    /// Keyboard-based movement with jump support for platformer gameplay.
    /// Implements IMovement for Strategy pattern.
    /// </summary>
    public class KeyboardMovement : IMovement
    {
        public float Speed { get; set; } = 5f;
        public float JumpForce { get; set; } = -12f;
        public bool IsGrounded { get; set; } = true;
        public bool FacingRight { get; private set; } = true;

        private bool _jumpPressed;
        private float _coyoteTime = 0.1f; // Allow jump shortly after leaving ground
        private float _coyoteTimer;

        public void Move(GameObject obj, GameTime gameTime)
        {
            float dt = gameTime.DeltaTime / 60f;
            
            // Update coyote time
            if (IsGrounded)
                _coyoteTimer = _coyoteTime;
            else
                _coyoteTimer -= dt;

            float velocityX = 0;

            if (Keyboard.IsKeyPressed(Key.LeftArrow) || Keyboard.IsKeyPressed(Key.A))
            {
                velocityX = -Speed;
                FacingRight = false;
            }

            if (Keyboard.IsKeyPressed(Key.RightArrow) || Keyboard.IsKeyPressed(Key.D))
            {
                velocityX = Speed;
                FacingRight = true;
            }

            // Jump with space or up arrow (only when grounded or within coyote time)
            bool jumpKeyDown = Keyboard.IsKeyPressed(Key.Space) || Keyboard.IsKeyPressed(Key.UpArrow) || Keyboard.IsKeyPressed(Key.W);
            bool canJump = IsGrounded || _coyoteTimer > 0;
            
            if (jumpKeyDown && !_jumpPressed && canJump)
            {
                obj.Velocity = new PointF(obj.Velocity.X, JumpForce);
                IsGrounded = false;
                _coyoteTimer = 0; // Consume coyote time
                SoundManager.Instance.Play(SoundType.PlayerJump);
            }
            _jumpPressed = jumpKeyDown;

            // Apply horizontal movement directly (not through velocity to avoid physics interference)
            obj.Position = new PointF(obj.Position.X + velocityX, obj.Position.Y);
        }

        public void SetGrounded(bool grounded)
        {
            IsGrounded = grounded;
        }
    }
}

