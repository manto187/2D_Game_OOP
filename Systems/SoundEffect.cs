using NAudio.Wave;
using FirstDesktopApp.Interfaces;

namespace FirstDesktopApp.Systems
{
    /// <summary>
    /// Represents a single sound effect that can be played.
    /// Single Responsibility: Only handles playing one sound file.
    /// </summary>
    public class SoundEffect : ISoundPlayer
    {
        private readonly string _filePath;
        private WaveOutEvent? _outputDevice;
        private AudioFileReader? _audioFile;
        private readonly object _lock = new();
        private bool _disposed;

        public bool IsPlaying => _outputDevice?.PlaybackState == PlaybackState.Playing;

        public SoundEffect(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Sound file not found: {filePath}");
            
            _filePath = filePath;
        }

        public void Play()
        {
            lock (_lock)
            {
                if (_disposed) return;

                // Stop and dispose previous playback if any
                StopInternal();

                try
                {
                    // Create new instances for each play
                    _audioFile = new AudioFileReader(_filePath);
                    _outputDevice = new WaveOutEvent();
                    _outputDevice.Init(_audioFile);
                    
                    // Auto-cleanup when playback stops
                    _outputDevice.PlaybackStopped += OnPlaybackStopped;
                    
                    _outputDevice.Play();
                }
                catch
                {
                    // Silently fail - don't crash game for audio issues
                    StopInternal();
                }
            }
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            // Cleanup after playback completes
            lock (_lock)
            {
                if (sender is WaveOutEvent device)
                {
                    device.PlaybackStopped -= OnPlaybackStopped;
                }
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                StopInternal();
            }
        }

        private void StopInternal()
        {
            try
            {
                _outputDevice?.Stop();
                _outputDevice?.Dispose();
                _audioFile?.Dispose();
            }
            catch { }
            finally
            {
                _outputDevice = null;
                _audioFile = null;
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_disposed) return;
                _disposed = true;
                StopInternal();
            }
        }
    }
}
