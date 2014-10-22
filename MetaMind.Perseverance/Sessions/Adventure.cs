using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using MetaMind.Engine;
using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Perseverance.Concepts;
using MetaMind.Perseverance.Screens;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Sessions
{
    [DataContract]
    public class Adventure : EngineObject
    {
        //---------------------------------------------------------------------
        public static readonly string XmlFilename = "adventure.xml";
        public static readonly string XmlPath = FolderManager.SaveFolderPath + XmlFilename;

        //---------------------------------------------------------------------
        private static Adventure singleton;

        //---------------------------------------------------------------------
        private ChamberScreen chamberScreen;
        private IWidget       adventureGui;

        //---------------------------------------------------------------------
        private Adventure( ChamberScreen chamberScreen )
        {
            //------------------------------------------------------------------
            // screen control
            this.chamberScreen = chamberScreen;

            //------------------------------------------------------------------
            // data
            Datebase = new Datebase();
        }

        //---------------------------------------------------------------------
        [DataMember]
        public Datebase Datebase { get; set; }
        //---------------------------------------------------------------------

        #region Gui Load

        public void LoadGui()
        {
            adventureGui = new AdventureGui();
        }

        #endregion Gui Load

        //---------------------------------------------------------------------

        #region Data Load and Save

        public static Adventure Load( ChamberScreen chamberScreen )
        {
            if ( File.Exists( XmlPath ) )
            {
                // auto-backup the old file
                File.Copy( XmlPath, XmlPath + ".bak", true );
                // load from save
                Load( chamberScreen, XmlPath );
            }
            else if ( File.Exists( XmlPath + ".bak" ) )
            {
                // load from the backup
                Load( chamberScreen, XmlPath + ".bak" );
            }
            else
            {
                // create a new singleton
                singleton = new Adventure( chamberScreen );
            }
            return singleton;
        }

        public void End()
        {
            // exit the mainscreen
            // store the flow, for re-entrance
            if ( singleton == null )
                return;
            singleton = null;

            if ( chamberScreen == null )
                return;
            chamberScreen.ExitScreen();
        }

        public void Save()
        {
            var serializer = new DataContractSerializer( typeof( Adventure ), null, int.MaxValue, false, true, null );
            using ( var file = File.Create( XmlPath ) )
                serializer.WriteObject( file, singleton );
        }

        private static void Load( ChamberScreen chamberScreen, string path )
        {
            using ( var file = File.OpenRead( path ) )
            {
                try
                {
                    var deserializer = new DataContractSerializer( typeof( Adventure ) );
                    using ( var reader = XmlDictionaryReader.CreateTextReader( file, new XmlDictionaryReaderQuotas() ) )
                        singleton = ( Adventure ) deserializer.ReadObject( reader, true );
                }
                catch ( SerializationException )
                {
                    singleton = new Adventure( chamberScreen );
                }
            }
        }

        #endregion Data Load and Save

        //---------------------------------------------------------------------

        #region Update and Draw

        public void Draw( GameTime gameTime )
        {
            adventureGui.Draw( gameTime );
        }

        public void HandleInput()
        {
            adventureGui.HandleInput();
        }

        public void Update( GameTime gameTime )
        {
            adventureGui.Update( gameTime );
        }

        #endregion Update and Draw

        //---------------------------------------------------------------------
    }
}