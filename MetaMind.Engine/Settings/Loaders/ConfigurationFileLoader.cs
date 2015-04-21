using System.Collections.Generic;

namespace MetaMind.Engine.Settings.Loaders
{
    using System.IO;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Parsers.Grammars;

    using Sprache;

    public static class ConfigurationFileLoader
    {
        #region Configuration Pair Loading

        public static List<KeyValuePair<string, string>> LoadDuplicablePairs(IConfigurationLoader loader)
        {
            var list = new List<KeyValuePair<string, string>>();

            var lines = LoadAllLine(loader);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = ConfigurationFileGrammar.ConfigurationLineParser.TryParse(line);
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

        public static Dictionary<string, string> LoadUniquePairs(IConfigurationLoader loader)
        {
            var list = new Dictionary<string, string>();

            var lines = LoadAllLine(loader);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = ConfigurationFileGrammar.ConfigurationLineParser.TryParse(line);
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

        private static string[] LoadAllLine(IConfigurationLoader loader)
        {
            return File.ReadAllLines(FolderManager.ConfigurationPath(loader));
        }

        #endregion

        #region Configuration Extraction

        public static bool ExtractBool(Dictionary<string, string> dict, string keyName, bool defaultValue)
        {
            bool value;
            var success = bool.TryParse(dict[keyName], out value);
            if (!success)
            {
                value = defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Get integer value from multiple int string separated by single white space.
        /// </summary>
        public static int ExtractMultipleInt(Dictionary<string, string> dict, string keyName, int index, int defaultValue)
        {
            int value;
            var success = int.TryParse(dict[keyName].Split(' ')[index], out value);
            if (!success)
            {
                value = defaultValue;
            }

            return value;
        }

        #endregion
    }
}