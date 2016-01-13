namespace MetaMind.Engine.Services.IO
{
    using Components.IO;
    using Parser.Grammar;
    using Sprache;
    using System.Collections.Generic;
    using System.IO;

    /// <example>
    /// A = 1 " A is something
    /// B = 1 " B is something
    /// ...
    /// </example>
    public static class PlainConfigurationLoader
    {
        #region Configuration Interface Loading

        public static List<KeyValuePair<string, string>> LoadDuplicable(IPlainConfigurationFileLoader fileLoader)
        {
            return LoadListPairs(LoadAllLines(fileLoader));
        }

        public static Dictionary<string, string> LoadUnique(IPlainConfigurationFileLoader fileLoader)
        {
            return LoadDictPairs(LoadAllLines(fileLoader));
        }

        #endregion

        #region Configuration Pair Loading

        private static string[] LoadAllLines(IPlainConfigurationFileLoader fileLoader)
        {
            return File.ReadAllLines(MMDirectoryManager.ConfigurationPath(fileLoader));
        }

        private static List<KeyValuePair<string, string>> LoadListPairs(string[] lines)
        {
            var list = new List<KeyValuePair<string, string>>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = ConfigurationGrammar.LineParser.TryParse(line);
                if (result.WasSuccessful)
                {
                    var pair = result.Value;

                    // Filter out comments
                    if (pair.Key != null)
                    {
                        list.Add(pair);
                    }
                }
            }

            return list;
        }

        private static Dictionary<string, string> LoadDictPairs(string[] lines)
        {
            var list = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = ConfigurationGrammar.LineParser.TryParse(line);
                if (result.WasSuccessful)
                {
                    var pair = result.Value;

                    // Filter out comments
                    if (pair.Key != null)
                    {
                        list.Add(pair.Key, pair.Value);
                    }
                }
            }

            return list;
        }

        #endregion

        #region Configuration Pair Reading

        #endregion

        #region Dictionary Helper Methods

        #endregion
    }
}