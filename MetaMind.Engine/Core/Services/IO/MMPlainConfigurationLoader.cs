namespace MetaMind.Engine.Core.Services.IO
{
    using System.Collections.Generic;
    using System.IO;
    using Backend.IO;
    using Parser.Grammar;
    using Sprache;

    /// <example>
    /// A = 1 " A is something
    /// B = 1 " B is something
    /// ...
    /// </example>
    public static class MMPlainConfigurationLoader
    {
        #region Configuration Interface Loading

        public static List<KeyValuePair<string, string>> LoadDuplicable(IMMPlainConfigurationFileLoader fileLoader)
        {
            return LoadListPairs(LoadAllLines(fileLoader));
        }

        public static Dictionary<string, string> LoadUnique(IMMPlainConfigurationFileLoader fileLoader)
        {
            return LoadDictPairs(LoadAllLines(fileLoader));
        }

        #endregion

        #region Configuration Pair Loading

        private static string[] LoadAllLines(IMMPlainConfigurationFileLoader fileLoader)
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