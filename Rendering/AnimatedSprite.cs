using System.Drawing;

namespace FirstDesktopApp.Rendering
{
    /// <summary>
    /// Manages multiple animations for a game object.
    /// Open/Closed: New animations can be added without modifying existing code.
    /// </summary>
    public class AnimatedSprite
    {
        private readonly Dictionary<string, SpriteAnimation> _animations = new();
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

        public void AddAnimation(string name, SpriteAnimation animation)
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

        public Image? GetCurrentFrame()
        {
            return _animations.TryGetValue(_currentAnimation, out var anim) ? anim.CurrentFrame : null;
        }

        public string CurrentAnimationName => _currentAnimation;
    }
}
