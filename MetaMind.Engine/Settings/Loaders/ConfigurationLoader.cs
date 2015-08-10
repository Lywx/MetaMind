namespace MetaMind.Engine.Settings.Loaders
{
    using System.Collections.Generic;
    using System.IO;
    using Components;

    public static class ConfigurationLoader
    {
        public static List<KeyValuePair<string, string>> LoadDuplicablePairs(IConfigurationLoader loader)
        {
            return FileLoader.LoadDuplicablePairs(LoadAllLine(loader));
        }

        public static Dictionary<string, string> LoadUniquePairs(IConfigurationLoader loader)
        {
            return FileLoader.LoadUniquePairs(LoadAllLine(loader));
        }

        private static string[] LoadAllLine(IConfigurationLoader loader)
        {
            return File.ReadAllLines(FileManager.ConfigurationPath(loader));
        }
    }
}