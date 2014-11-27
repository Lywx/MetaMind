namespace MetaMind.Acutance.Sessions
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components;

    /// <summary>
    /// Adventure is a data class.
    /// </summary>
    [DataContract]
    public class Adventure : EngineObject
    {
        #region File Storage

        [DataMember]
        public const string XmlFilename = "Adventure.xml";

        [DataMember]
        public const string XmlPath = FolderManager.SaveFolderPath + XmlFilename;

        #endregion File Storage

        #region Singleton

        private static Adventure singleton;

        #endregion Singleton

        #region Constructors

        private Adventure()
        {
            this.Random    = new Random((int)DateTime.Now.Ticks);
            this.Tracelist = new Tracelist();
        }

        #endregion Constructors

        #region Public Properties

        [DataMember]
        public Tracelist Tracelist { get; private set; }

        public Random Random { get; private set; }

        #endregion Public Properties

        #region Load and Save

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.Random = new Random((int)DateTime.Now.Ticks);
        }

        public static Adventure LoadSave()
        {
            if (File.Exists(XmlPath))
            {
                // auto-backup the old file
                File.Copy(XmlPath, XmlPath + ".bak", true);

                // load from save
                LoadSave(XmlPath);
            }
            else if (File.Exists(XmlPath + ".bak"))
            {
                // load from the backup
                LoadSave(XmlPath + ".bak");
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
            var serializer = new DataContractSerializer(typeof(Adventure), null, int.MaxValue, false, true, null);
            using (var file = File.Create(XmlPath))
            {
                serializer.WriteObject( file, singleton );
            }
        }

        private static void LoadSave(string path)
        {
            using (var file = File.OpenRead(path))
            {
                try
                {
                    var deserializer = new DataContractSerializer(typeof(Adventure));
                    using (var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas()))
                    {
                        singleton = (Adventure)deserializer.ReadObject(reader, true);
                    }
                }
                catch (SerializationException)
                {
                    singleton = new Adventure();
                }
            }
        }

        #endregion Load and Save

        #region Update 

        public void Update()
        {
            this.Tracelist.Update();
        }

        #endregion Update 

        //---------------------------------------------------------------------
    }
}