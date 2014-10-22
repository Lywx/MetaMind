using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components.Files
{
    [DataContract]
    public class Folder : EngineObject
    {
        #region Data

        [DataMember]
        public string DirPath { get; private set; }

        [DataMember]
        public bool Enabled { get; private set; }

        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Symbol { get; private set; }

        #endregion Data

        #region Constructors

        public Folder(string dirPath)
        {
            DirPath = dirPath;

            var folderName = DirPath.Replace(Components.FolderManager.DataFolderPath, "").Replace(@"\", "");
            var folderElems = BreakFolderNameIntoElems(folderName);

            Symbol = folderElems[0];
            Id = int.Parse(folderElems[1]);
            Name = folderElems[2];

            Enabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Folder"/> class
        /// for new items.
        /// </summary>
        /// <param name="symbol">The folder symbol string.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The folder name.</param>
        public Folder(string symbol, int id, string name)
        {
            Symbol = symbol;
            Id = id;
            Name = name;

            DirPath = ParseDirPath(symbol, id, name);

            Enabled = false;
        }

        #endregion Constructors

        #region Operation

        public void Delete()
        {
            if (!Enabled)
                return;

            FileSystem.DeleteDirectory(DirPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }

        public void Enable()
        {
            if (!FileSystem.DirectoryExists(DirPath))
                FileSystem.CreateDirectory(DirPath);
            Enabled = true;
        }

        public bool MoveToRepository(string repositoryPath = null)
        {
            if (!RepositoryReady || ReposedAlready)
                return false;

            //if (repositoryPath != null)
            //    var dirPathInRepository = DirPath.Replace(FolderManager.DataFolderPath, repositoryPath);
            //else
            var dirPathInRepository = DirPath.Replace((string)Components.FolderManager.DataFolderPath, Components.FolderManager.RepositoryFolderPath);

            FileSystem.MoveDirectory(DirPath, dirPathInRepository, UIOption.AllDialogs);
            DirPath = dirPathInRepository;

            return true;
        }

        private bool ReposedAlready
        {
            get { return DirPath.Contains(Components.FolderManager.RepositoryFolderPath); }
        }

        private static bool RepositoryReady
        {
            get { return FileSystem.DirectoryExists(Components.FolderManager.RepositoryFolderPath); }
        }

        public void OpenInExplorer()
        {
            if (!Enabled)
            {
                MessageManager.PopMessages("Folder does not exist.");
                return;
            }
            Process.Start(DirPath);
        }

        public void SynchronizePath( object sender, ViewItemDataEventArgs e )
        {
            if ( !Enabled )
            {
                MessageManager.PopMessages( "Folder does not exist." );
                return;
            }
            Name = FontManager.GetDisaplayableCharacters( Font.InfoSimSunFont, e.NewValue );
            // only rename when the name is actually changed
            // considering files and folders are case-insensitive.
            // always rename folders to title case
            var textInfo = new CultureInfo( "en-US", false ).TextInfo;
            var oldPath = DirPath.ToLower();
            var newPath = ParseDirPath( Symbol, Id, Name ).ToLower();
            if ( !string.Equals( oldPath, newPath ) )
            {
                FileSystem.RenameDirectory( DirPath, ParseFolderName( Symbol, Id, textInfo.ToTitleCase( Name ) ) );
            }
            DirPath = ParseDirPath( Symbol, Id, Name );
        }

        #endregion Operation

        #region Update

        public void Update(GameTime gameTime)
        {
            Enabled = FileSystem.DirectoryExists(DirPath);
        }

        #endregion Update

        #region Helper Parser

        /// <summary>
        /// Parsers the name of the folder to symbol, id and name.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        public static string[] BreakFolderNameIntoElems(string folderName)
        {
            var rawElems = folderName.Split(' ');
            var symbol = rawElems[0];
            var id = rawElems[1];
            var name = string.Join(" ", rawElems.Skip(2));

            string[] folderElems = { symbol, id, name };

            return folderElems;
        }

        private static string ParseFolderName(string symbol, int id, string name)
        {
            return string.Format("{0} {1} {2}", symbol, id, name).Trim();
        }

        private static string ParseDirPath(string symbol, int id, string name)
        {
            return string.Format(Components.FolderManager.DataFolderPath + ParseFolderName(symbol, id, name) + "\\");
        }

        #endregion Helper Parser
    }
}