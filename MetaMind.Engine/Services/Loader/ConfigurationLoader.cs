namespace MetaMind.Engine.Services.Loader
{
    using System.Collections.Generic;
    using System.IO;
    using Components.IO;

    public static class ConfigurationLoader
    {
        public static List<KeyValuePair<string, string>> LoadDuplicablePairs(IConfigurable configurable)
        {
            return InformationLoader.ReadListPairs(LoadAllLine(configurable));
        }

        public static Dictionary<string, string> LoadUniquePairs(IConfigurable configurable)
        {
            return InformationLoader.LoadDictPairs(LoadAllLine(configurable));
        }

        private static string[] LoadAllLine(IConfigurable configurable)
        {
            return File.ReadAllLines(MMDirectoryManager.ConfigurationPath(configurable));
        }
    }
}