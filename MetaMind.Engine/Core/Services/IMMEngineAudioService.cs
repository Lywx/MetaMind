namespace MetaMind.Engine.Core.Services
{
    using Backend.Audio;

    public interface IMMEngineAudioService
    {
        #region Manager and Settings

        IMMAudioManager Manager { get; }

        MMAudioSettings Settings { get; }

        #endregion

        #region Controller

        IMMAudioController Controller { get; }

        MMAudioDeviceController DeviceController { get; }

        #endregion
    }
}