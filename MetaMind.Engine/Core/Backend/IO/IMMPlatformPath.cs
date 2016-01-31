namespace MetaMind.Engine.Core.Backend.IO
{
    public interface IMMPlatformPath
    {
        string ConfigurationDirectory { get; }

        string ContentDirectory { get; }

        string DataDirectory { get; }

        string SaveDirectory { get; }
    }
}