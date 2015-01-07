using System.Collections.Generic;

namespace MetaMind.Engine.Settings.Loaders
{
    using System.IO;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Parsers.Grammars;

    using Sprache;

    public static class ConfigurationLoader
    {
        public static bool BooleanValue(Dictionary<string, string> dict, string keyName, bool defaultValue)
        {
            bool value;
            var success = bool.TryParse(dict[keyName], out value);
            if (!success)
            {
                value = defaultValue;
            }

            return value;
        }

        public static Dictionary<string, string> LoadUniquePairs(IConfigurationLoader loader)
        {
            var list = new Dictionary<string, string>();

            var lines = File.ReadAllLines(FolderManager.ConfigurationPath(loader));
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = ConfigurationParser.ConfigurationLineParser.TryParse(line);
                if (result.WasSuccessful)
                {
                    var pair = result.Value;

                    // filter out comments
                    if (pair.Key != null)
                    {
                        list.Add(pair.Key, pair.Value);
                    }
                }
            }

            return list;
        }

        public static List<KeyValuePair<string, string>> LoadDuplicablePairs(IConfigurationLoader loader)
        {
            var list = new List<KeyValuePair<string, string>>();

            var lines = File.ReadAllLines(FolderManager.ConfigurationPath(loader));
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = ConfigurationParser.ConfigurationLineParser.TryParse(line);
                if (result.WasSuccessful)
                {
                    var pair = result.Value;

                    // filter out comments
                    if (pair.Key != null)
                    {
                        list.Add(pair);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get integer value from multiple int string separated by single white space.
        /// </summary>
        public static int MultipleIntValue(Dictionary<string, string> dict, string keyName, int index, int defaultValue)
        {
            int value;
            var success = int.TryParse(dict[keyName].Split(' ')[index], out value);
            if (!success)
            {
                value = defaultValue;
            }

            return value;
        }

        private static string[] LoadAllLine(IConfigurationLoader loader)
        {
            return File.ReadAllLines(FolderManager.ConfigurationPath(loader));
        }
    }
}