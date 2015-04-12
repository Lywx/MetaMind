namespace MetaMind.Engine.Settings.Loaders
{
    public interface IConfigurationFileLoader
    {
        string ConfigurationFile { get; }

        void LoadConfiguration();
    }
}