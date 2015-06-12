namespace MetaMind.Testimony.Scripting
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Engine.Components;
    using Engine.Settings.Loaders;

    public class ScriptSearcher : IConfigurationLoader
    {
        private readonly string searchPattern = "*.fsx";

        private string searchFolder;

        public ScriptSearcher()
        {
            this.LoadConfiguration();
        }


        public List<string> SearchScriptPaths()
        {
            return this.SearchScriptPaths(this.searchFolder);
        }

        private List<string> SearchScriptPaths(string searchPath)
        {
            return Directory.GetFiles(searchPath, this.searchPattern, SearchOption.AllDirectories).ToList();
        }

        #region Configuration

        public string ConfigurationFile
        {
            get { return "Test.txt"; }
        }

        public void LoadConfiguration()
        {
            var pairs = ConfigurationFileLoader.LoadUniquePairs(this);
            this.searchFolder = FileManager.DataPath(pairs["TestFolder"]);;
        }

        #endregion
    }
}