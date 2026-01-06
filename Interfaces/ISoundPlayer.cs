namespace FirstDesktopApp.Interfaces
{
    /// <summary>
    /// Interface for sound playback (Interface Segregation Principle).
    /// </summary>
    public interface ISoundPlayer : IDisposable
    {
        void Play();
        void Stop();
        bool IsPlaying { get; }
    }
}
