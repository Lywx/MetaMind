namespace MetaMind.Engine.Sessions
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework;

    [DataContract]
    public class Session<TData> : ISession<TData>
        where TData : ISessionData, new()
    {
        #region File Data

        [DataMember]
        public const string XmlFilename = "Session.xml";

        [DataMember]
        public const string XmlPath = FolderManager.SaveFolderPath + XmlFilename;

        #endregion 

        #region Session Data 

        [DataMember]
        private TData Data { get; set; }

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

        public static Session<TData> LoadSave()
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
                // skip because there may be other instance trying to save
            }
        }

        private static void LoadSave(string path)
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
            }
        }

        #endregion 

        #region Update

        public virtual void Update(GameTime gameTime)
        {
            this.Data.Update(gameTime);
        }

        #endregion Update
    }
}