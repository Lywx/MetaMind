namespace MetaMind.Engine.Services.Loader
{
    public interface IConfigurationLoader
    {
        string ConfigurationFile { get; }

        void LoadConfiguration();
    }
}