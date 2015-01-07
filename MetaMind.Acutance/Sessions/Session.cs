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
    /// Session is a data class.
    /// </summary>
    [DataContract,
    KnownType(typeof(Commandlist)),
    KnownType(typeof(Modulelist))]
    public class Session : EngineObject
    {
        #region File Storage

        [DataMember]
        public const string XmlFilename = "Session.xml";

        [DataMember]
        public const string XmlPath = FolderManager.SaveFolderPath + XmlFilename;

        #endregion File Storage

        #region Singleton

        private static Session singleton;

        #endregion Singleton

        #region Constructors

        private Session()
        {
            this.Random = new Random((int)DateTime.Now.Ticks);

            this.Commandlist = new Commandlist();
            this.Modulelist  = new Modulelist(this.Commandlist);
        }

        #endregion Constructors

        #region Public Properties

        public Random Random { get; private set; }


        [DataMember(Name = "Modulelist")]
        public IModulelist Modulelist { get; private set; }

        [DataMember(Name = "Commandlist")]
        public ICommandlist Commandlist { get; private set; }

        #endregion Public Properties

        #region Load and Save

        public static Session LoadSave()
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
                singleton = new Session();
            }

            return singleton;
        }

        public void Save()
        {
            var serializer = new DataContractSerializer(typeof(Session), null, int.MaxValue, false, true, null);
            using (var file = File.Create(XmlPath))
            {
                serializer.WriteObject(file, singleton);
            }
        }

        private static void LoadSave(string path)
        {
            using (var file = File.OpenRead(path))
            {
                try
                {
                    var deserializer = new DataContractSerializer(typeof(Session));
                    using (var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas()))
                    {
                        singleton = (Session)deserializer.ReadObject(reader, true);
                    }
                }
                catch (SerializationException)
                {
                    singleton = new Session();
                }
            }
        }

        #endregion Load and Save

        #region Update

        public void Update()
        {
            this.Modulelist .Update();
            this.Commandlist.Update();
        }

        #endregion Update

        #region Serialization

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (this.Random == null)
            {
                this.Random = new Random((int)DateTime.Now.Ticks);
            }

            if (this.Commandlist == null)
            {
                this.Commandlist = new Commandlist();
            }

            if (this.Modulelist == null)
            {
                this.Modulelist = new Modulelist(this.Commandlist);
            }

        }

        #endregion
    }
}