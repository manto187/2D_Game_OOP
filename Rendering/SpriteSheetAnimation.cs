namespace FirstDesktopApp.Rendering
{
    /// <summary>
    /// Handles sprite sheet animations (single image with multiple frames in a row).
    /// </summary>
    public class SpriteSheetAnimation
    {
        private readonly Image _spriteSheet;
        private readonly int _frameCount;
        private readonly int _frameWidth;
        private readonly int _frameHeight;
        private readonly float _frameTime;
        private int _currentFrame;
        private float _frameTimer;

        public bool IsFlipped { get; set; }

        public SpriteSheetAnimation(Image spriteSheet, int frameCount, float frameTime = 0.1f)
        {
            _spriteSheet = spriteSheet;
            _frameCount = frameCount;
            _frameWidth = spriteSheet.Width / frameCount;
            _frameHeight = spriteSheet.Height;
            _frameTime = frameTime;
        }

        public void Update(float deltaTime)
        {
            _frameTimer += deltaTime;
            if (_frameTimer >= _frameTime)
            {
                _frameTimer = 0;
                _currentFrame = (_currentFrame + 1) % _frameCount;
            }
        }

        public void Reset()
        {
            _currentFrame = 0;
            _frameTimer = 0;
        }

        public void Draw(Graphics g, RectangleF destRect)
        {
            var srcRect = new Rectangle(_currentFrame * _frameWidth, 0, _frameWidth, _frameHeight);
            
            if (IsFlipped)
            {
                g.TranslateTransform(destRect.X + destRect.Width / 2, destRect.Y + destRect.Height / 2);
                g.ScaleTransform(-1, 1);
                g.DrawImage(_spriteSheet, 
                    new RectangleF(-destRect.Width / 2, -destRect.Height / 2, destRect.Width, destRect.Height),
                    srcRect, GraphicsUnit.Pixel);
                g.ResetTransform();
            }
            else
            {
                g.DrawImage(_spriteSheet, destRect, srcRect, GraphicsUnit.Pixel);
            }
        }

        public int FrameCount => _frameCount;
    }
}
