using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Movements
{
    /// <summary>
    /// Patrol movement for enemies - moves back and forth between bounds.
    /// Implements IMovement for Strategy pattern.
    /// </summary>
    public class PatrolMovement : IMovement
    {
        private readonly float _leftBound;
        private readonly float _rightBound;
        private float _speed;

        public bool FacingRight => _speed > 0;

        public PatrolMovement(float left, float right, float speed = 2f)
        {
            _leftBound = left;
            _rightBound = right;
            _speed = speed;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            obj.Position = new PointF(obj.Position.X + _speed, obj.Position.Y);

            if (obj.Position.X <= _leftBound)
            {
                obj.Position = new PointF(_leftBound, obj.Position.Y);
                _speed = Math.Abs(_speed);
            }
            else if (obj.Position.X >= _rightBound)
            {
                obj.Position = new PointF(_rightBound, obj.Position.Y);
                _speed = -Math.Abs(_speed);
            }
        }
    }
}
