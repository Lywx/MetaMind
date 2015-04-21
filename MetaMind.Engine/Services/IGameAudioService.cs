namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;

    public interface IGameAudioService
    {
        IAudioManager Audio { get; }
    }
}