namespace MetaMind.Testimony.Guis.Modules
{
    using Engine.Settings.Loaders;

    public class TestSettings : IConfigurationLoader
    {
        public string ConfigurationFile { get { return "Folder.txt"; } }

        public void LoadConfiguration()
        {
            
        }
    }
}