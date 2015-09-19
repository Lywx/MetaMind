namespace MetaMind.Engine.Setting.Loader
{
    public interface IConfigurationLoader
    {
        string ConfigurationFile { get; }

        void LoadConfiguration();
    }
}