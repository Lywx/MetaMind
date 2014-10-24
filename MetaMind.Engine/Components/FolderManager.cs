using System.IO;

namespace MetaMind.Engine.Components
{
    public class FolderManager
    {
        #region Directory Settings

        public const string DataFolderPath = @".\Data\";

        public const string SaveFolderPath = @".\Save\";

        #endregion Directory Settings

        #region Singleton

        private static FolderManager singleton;

        public static FolderManager GetInstance()
        {
            return singleton ?? ( singleton = new FolderManager() );
        }

        #endregion Singleton

        #region Constructors

        private FolderManager()
        {
            CreateDirectory();
        }

        private void CreateDirectory()
        {
            if ( !Directory.Exists( SaveFolderPath ) )
            {
                // if it doesn't exist, create
                Directory.CreateDirectory( SaveFolderPath );
            }

            if ( !Directory.Exists( DataFolderPath ) )
                Directory.CreateDirectory( DataFolderPath );
        }

        #endregion Constructors
    }
}