namespace MetaMind.Engine.Core.Services.Script.FSharp
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Backend.IO;
    using Entity.Common;
    using IO;

    public class FsScriptSearcher : MMEntity, IMMPlainConfigurationFileLoader
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
            var configuration = MMPlainConfigurationLoader.LoadUnique(this);
            this.SearchFolder = MMDirectoryManager.DataPath(configuration["FsScriptSearcher.SearchFolder"]); ;
        }

        #endregion
    }
}