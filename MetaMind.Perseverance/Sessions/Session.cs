namespace MetaMind.Perseverance.Sessions
{
    using MetaMind.Engine.Components;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.Motivations;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    /// <summary>
    /// Session is a data class.
    /// </summary>
    [DataContract]
    public class Session
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
            this.LoadRandomNumberGenerator();

            this.Cognition = new Cognition();
            this.Motivation = new MotivationStorage();
        }

        #endregion Constructors

        #region Public Properties

        [DataMember(Name = "Cognition")]
        public Cognition Cognition { get; private set; }

        [DataMember(Name = "Motivation")]
        public MotivationStorage Motivation { get; private set; }

        public Random Random { get; private set; }

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

        [OnDeserialized]
        public void LoadRandomNumberGenerator(StreamingContext context)
        {
            this.LoadRandomNumberGenerator();
        }

        public void Save()
        {
            var serializer = new DataContractSerializer(typeof(Session), null, int.MaxValue, false, true, null);
            try
            {
                using (var file = File.Create(XmlPath))
                {
                    serializer.WriteObject(file, singleton);
                }
            }
            catch (IOException)
            {
                // skip because there may be other instance trying to save
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

        private void LoadRandomNumberGenerator()
        {
            this.Random = new Random((int)DateTime.Now.Ticks);
        }

        #endregion Load and Save

        #region Update

        public void Update()
        {
            this.Cognition.Update();
        }

        #endregion Update
    }
}