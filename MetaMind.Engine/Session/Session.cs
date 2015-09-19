namespace MetaMind.Engine.Session
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;
    using Component.File;

    [DataContract]
    public sealed class Session<TData> : ISession<TData>
        where TData : ISessionData, new()
    {
        #region File Data

        [DataMember]
        public const string XmlFilename = "Session.xml";

        [DataMember]
        public const string XmlPath = FileManager.SaveFolderPath + XmlFilename;

        #endregion 

        #region Session Data 

        [DataMember]
        public TData Data { get; set; }

        #endregion

        #region Singleton

        private static Session<TData> Singleton { get; set; }

        #endregion Singleton

        #region Constructors

        private Session()
        {
            this.Data = new TData();
        }

        #endregion Constructors

        #region Save and Load

        public static Session<TData> Load()
        {
            if (File.Exists(XmlPath))
            {
                // Auto-backup the old file
                File.Copy(XmlPath, XmlPath + ".bak", true);

                // Load from save
                Load(XmlPath);
            }
            else if (File.Exists(XmlPath + ".bak"))
            {
                // Load from the backup
                Load(XmlPath + ".bak");
            }
            else
            {
                // Create a new singleton
                Singleton = new Session<TData>();
            }

            return Singleton;
        }

        public void Save()
        {
            var serializer = new DataContractSerializer(typeof(Session<TData>), null, int.MaxValue, false, true, null);
            try
            {
                using (var file = File.Create(XmlPath))
                {
                    serializer.WriteObject(file, Singleton);
                }
            }
            catch (IOException)
            {
                // Skip because there may be other instance trying to save
            }
        }

        private static void Load(string path)
        {
            using (var file = File.OpenRead(path))
            {
                try
                {
                    var deserializer = new DataContractSerializer(typeof(Session<TData>));
                    using (var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas()))
                    {
                        Singleton = (Session<TData>)deserializer.ReadObject(reader, true);
                    }
                }
                catch (SerializationException)
                {
                    Singleton = new Session<TData>();
                }
                catch (XmlException)
                {
                    Singleton = new Session<TData>();
                }
            }
        }

        #endregion 

        #region Update

        public void Update()
        {
            this.Data.Update();
        }

        #endregion Update
    }
}