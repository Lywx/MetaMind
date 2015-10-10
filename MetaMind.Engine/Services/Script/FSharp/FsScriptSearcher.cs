namespace MetaMind.Engine.Services.Script.FSharp
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Components.IO;
    using Entities;
    using Loader;

    public class FsScriptSearcher : MMEntity, IConfigurationLoader
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

        public string ConfigurationFile => "Session.txt";

        public void LoadConfiguration()
        {
            var configuration = ConfigurationLoader.LoadUniquePairs(this);
            this.SearchFolder = FileManager.DataPath(configuration["FsScriptSearcher.SearchFolder"]); ;
        }

        #endregion
    }
}