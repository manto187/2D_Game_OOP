namespace FirstDesktopApp.Systems
{
    /// <summary>
    /// Manages all game sounds following SOLID principles.
    /// - Single Responsibility: Only manages sound playback coordination
    /// - Open/Closed: New sounds can be added without modifying this class
    /// - Dependency Inversion: Depends on ISoundPlayer abstraction
    /// </summary>
    public class SoundManager : IDisposable
    {
        private static SoundManager? _instance;
        private static readonly object _instanceLock = new();
        
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        _instance ??= new SoundManager();
                    }
                }
                return _instance;
            }
        }

        private readonly Dictionary<SoundType, SoundEffect> _sounds = new();
        private bool _initialized;
        private bool _muted;
        private bool _disposed;

        private SoundManager() { }

        /// <summary>
        /// Initialize the sound manager with sound files from the resource path.
        /// </summary>
        public void Initialize(string resourcePath)
        {
            if (_initialized || _disposed) return;

            var soundFiles = new Dictionary<SoundType, string>
            {
                { SoundType.PlayerShoot, "firing.mp3" },
                { SoundType.PlayerJump, "jump.mp3" },
                { SoundType.EnemySpell, "magic-spell.mp3" },
                { SoundType.EnemyDeath, "falling.mp3" },
                { SoundType.HealthPickup, "health.mp3" }
            };

            foreach (var kvp in soundFiles)
            {
                var fullPath = Path.Combine(resourcePath, kvp.Value);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        _sounds[kvp.Key] = new SoundEffect(fullPath);
                    }
                    catch
                    {
                        // Skip sounds that fail to load
                    }
                }
            }

            _initialized = true;
        }

        /// <summary>
        /// Play a sound effect once.
        /// </summary>
        public void Play(SoundType soundType)
        {
            if (!_initialized || _muted || _disposed) return;

            if (_sounds.TryGetValue(soundType, out var sound))
            {
                sound.Play();
            }
        }

        /// <summary>
        /// Stop a specific sound.
        /// </summary>
        public void Stop(SoundType soundType)
        {
            if (_sounds.TryGetValue(soundType, out var sound))
            {
                sound.Stop();
            }
        }

        /// <summary>
        /// Stop all sounds.
        /// </summary>
        public void StopAll()
        {
            foreach (var sound in _sounds.Values)
            {
                sound.Stop();
            }
        }

        /// <summary>
        /// Toggle mute state.
        /// </summary>
        public void ToggleMute()
        {
            _muted = !_muted;
            if (_muted) StopAll();
        }

        /// <summary>
        /// Set mute state.
        /// </summary>
        public bool IsMuted
        {
            get => _muted;
            set
            {
                _muted = value;
                if (_muted) StopAll();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            foreach (var sound in _sounds.Values)
            {
                sound.Dispose();
            }
            _sounds.Clear();
            _instance = null;
        }
    }
}
