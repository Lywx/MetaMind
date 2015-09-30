namespace MetaMind.Engine.Service.Loader
{
    using System.Collections.Generic;
    using System.IO;
    using Components.File;

    public static class ConfigurationLoader
    {
        public static List<KeyValuePair<string, string>> LoadDuplicablePairs(IConfigurationLoader loader)
        {
            return FileLoader.ReadListPairs(LoadAllLine(loader));
        }

        public static Dictionary<string, string> LoadUniquePairs(IConfigurationLoader loader)
        {
            return FileLoader.LoadDictPairs(LoadAllLine(loader));
        }

        private static string[] LoadAllLine(IConfigurationLoader loader)
        {
            return File.ReadAllLines(FileManager.ConfigurationPath(loader));
        }
    }
}