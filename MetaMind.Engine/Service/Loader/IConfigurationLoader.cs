namespace MetaMind.Engine.Service.Loader
{
    public interface IConfigurationLoader
    {
        string ConfigurationFile { get; }

        void LoadConfiguration();
    }
}