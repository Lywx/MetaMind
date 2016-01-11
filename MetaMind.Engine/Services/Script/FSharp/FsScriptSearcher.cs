namespace MetaMind.Engine.Services.Script.FSharp
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Components.IO;
    using Entities;
    using Entities.Bases;
    using Loader;

    public class FsScriptSearcher : MMEntity, IConfigurable
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

        public string ConfigurationFilename => "Session.txt";

        public void LoadConfiguration()
        {
            var configuration = ConfigurationLoader.LoadUniquePairs(this);
            this.SearchFolder = MMDirectoryManager.DataPath(configuration["FsScriptSearcher.SearchFolder"]); ;
        }

        #endregion
    }
}