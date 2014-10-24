﻿using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using MetaMind.Engine.Components;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Sessions
{
    [DataContract]
    public class Adventure
    {
        #region File Storage

        [DataMember]
        public const string XmlFilename = "Adventure.xml";

        [DataMember]
        public const string XmlPath = FolderManager.SaveFolderPath + XmlFilename;

        #endregion File Storage

        //---------------------------------------------------------------------

        #region Singleton

        private static Adventure singleton;

        #endregion Singleton

        //---------------------------------------------------------------------

        #region Concepts

        [DataMember]
        private Cognition cognition;

        [DataMember]
        private Tasklist tasklist;

        #endregion Concepts

        //---------------------------------------------------------------------

        #region Constructors

        private Adventure()
        {
            tasklist = new Tasklist();
            cognition = new Cognition();
        }

        #endregion Constructors

        //---------------------------------------------------------------------

        #region Public Properties

        public Cognition Cognition { get { return cognition; } }

        public Tasklist Tasklist { get { return tasklist; } }

        #endregion Public Properties

        //---------------------------------------------------------------------

        #region Load and Save

        public static Adventure LoadSave()
        {
            if ( File.Exists( XmlPath ) )
            {
                // auto-backup the old file
                File.Copy( XmlPath, XmlPath + ".bak", true );
                // load from save
                LoadSave( XmlPath );
            }
            else if ( File.Exists( XmlPath + ".bak" ) )
            {
                // load from the backup
                LoadSave( XmlPath + ".bak" );
            }
            else
            {
                // create a new singleton
                singleton = new Adventure();
            }
            return singleton;
        }

        public void Save()
        {
            var serializer = new DataContractSerializer( typeof( Adventure ), null, int.MaxValue, false, true, null );
            using ( var file = File.Create( XmlPath ) )
                serializer.WriteObject( file, singleton );
        }

        private static void LoadSave( string path )
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
                    singleton = new Adventure();
                }
            }
        }

        #endregion Load and Save

        //---------------------------------------------------------------------

        #region Update and Draw

        public void Update( GameTime gameTime )
        {
            cognition.Update( gameTime );
        }

        #endregion Update and Draw

        //---------------------------------------------------------------------
    }
}