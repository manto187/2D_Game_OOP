using System.Drawing;

namespace FirstDesktopApp.Rendering
{
    /// <summary>
    /// Handles sprite animation with frame timing.
    /// Single Responsibility: Only manages animation frames and timing.
    /// </summary>
    public class SpriteAnimation
    {
        private readonly Image[] _frames;
        private int _currentFrame;
        private float _frameTimer;
        private readonly float _frameTime;

        public bool IsFlipped { get; set; }

        public SpriteAnimation(Image[] frames, float frameTime = 0.1f)
        {
            _frames = frames;
            _frameTime = frameTime;
            _currentFrame = 0;
            _frameTimer = 0;
        }

        public void Update(float deltaTime)
        {
            _frameTimer += deltaTime;
            if (_frameTimer >= _frameTime)
            {
                _frameTimer = 0;
                _currentFrame = (_currentFrame + 1) % _frames.Length;
            }
        }

        public void Reset()
        {
            _currentFrame = 0;
            _frameTimer = 0;
        }

        public Image CurrentFrame => _frames[_currentFrame];
        public int FrameCount => _frames.Length;
    }
}
