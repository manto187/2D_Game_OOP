namespace FirstDesktopApp.Rendering
{
    /// <summary>
    /// Manages sprite sheet animations for enemies.
    /// </summary>
    public class EnemyAnimatedSprite
    {
        private readonly Dictionary<string, SpriteSheetAnimation> _animations = new();
        private string _currentAnimation = "";
        private bool _isFlipped;

        public bool IsFlipped
        {
            get => _isFlipped;
            set
            {
                _isFlipped = value;
                if (_animations.TryGetValue(_currentAnimation, out var anim))
                    anim.IsFlipped = value;
            }
        }

        public void AddAnimation(string name, SpriteSheetAnimation animation)
        {
            _animations[name] = animation;
            if (string.IsNullOrEmpty(_currentAnimation))
                _currentAnimation = name;
        }

        public void Play(string name)
        {
            if (_currentAnimation != name && _animations.ContainsKey(name))
            {
                _animations[_currentAnimation]?.Reset();
                _currentAnimation = name;
                _animations[_currentAnimation].IsFlipped = _isFlipped;
            }
        }

        public void Update(float deltaTime)
        {
            if (_animations.TryGetValue(_currentAnimation, out var anim))
                anim.Update(deltaTime);
        }

        public void Draw(Graphics g, RectangleF bounds)
        {
            if (_animations.TryGetValue(_currentAnimation, out var anim))
                anim.Draw(g, bounds);
        }

        public string CurrentAnimationName => _currentAnimation;
    }
}
