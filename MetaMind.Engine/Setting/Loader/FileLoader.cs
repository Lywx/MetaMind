namespace MetaMind.Engine.Setting.Loader
{
    using System.Collections.Generic;
    using Parser.Grammar;
    using Sprache;

    public static class FileLoader
    {
        #region Configuration Pair Loading

        public static List<KeyValuePair<string, string>> ReadListPairs(string[] lines)
        {
            var list = new List<KeyValuePair<string, string>>();

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

        public static Dictionary<string, string> LoadDictPairs(string[] lines)
        {
            var list = new Dictionary<string, string>();

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

        #endregion

        #region Configuration Extraction

        public static bool ReadBool(Dictionary<string, string> dict, string key, bool @default = false)
        {
            bool value;
            var success = bool.TryParse(dict[key], out value);
            if (!success)
            {
                value = @default;
            }

            return value;
        }

        /// <summary>
        /// Get integer value from multiple int string separated by single white space.
        /// </summary>
        public static int ReadInts(Dictionary<string, string> dict, string key, int index, int @default = 0)
        {
            int value;
            var success = int.TryParse(ValueAt(dict, key, index), out value);
            if (!success)
            {
                value = @default;
            }

            return value;
        }

        public static float ReadFloats(Dictionary<string, string> dict, string key, int index, float @default = 0f)
        {
            float value;
            var success = float.TryParse(ValueAt(dict, key, index), out value);
            if (!success)
            {
                value = @default;
            }

            return value;
        }

        private static string ValueAt(Dictionary<string, string> dict, string key, int index)
        {
            return dict[key].Split(' ')[index];
        }

        #endregion
    }
}