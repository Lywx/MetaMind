namespace MetaMind.Engine.Components.IO
{
    public interface IMMPlatformPath
    {
        string ConfigurationDirectory { get; }

        string ContentDirectory { get; }

        string DataDirectory { get; }

        string SaveDirectory { get; }
    }
}