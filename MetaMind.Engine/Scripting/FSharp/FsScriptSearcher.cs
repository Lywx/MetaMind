namespace MetaMind.Engine.Scripting.FSharp
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Component.File;
    using Setting.Loader;

    public class FsScriptSearcher : IConfigurationLoader
    {
        private readonly string searchPattern = "*.fsx";

        public FsScriptSearcher()
        {
            this.LoadConfiguration();
        }

        private string SearchFolder { get; set; }

        public List<string> SearchScriptPaths()
        {
            return this.SearchScriptPaths(this.SearchFolder);
        }

        private List<string> SearchScriptPaths(string searchPath)
        {
            return Directory.GetFiles(searchPath, this.searchPattern, SearchOption.AllDirectories).ToList();
        }

        #region Configurations

        public string ConfigurationFile => "Unity.txt";

        public void LoadConfiguration()
        {
            var pairs         = ConfigurationLoader.LoadUniquePairs(this);
            this.SearchFolder = FileManager.DataPath(pairs["FsScriptSearcher.SearchFolder"]); ;
        }

        #endregion
    }
}