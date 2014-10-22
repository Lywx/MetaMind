using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace MetaMind.Engine.Components
{
    [DataContract]
    public class FolderManager
    {
        #region Directory Settings

        [DataMember]
        public static readonly string RepositoryFolderPath = @"E:\Experience\";
        [DataMember]
        public static readonly string DataFolderPath = @".\Data\";
        [DataMember]
        public static readonly string SaveFolderPath = @".\Save\";

        #endregion Directory Settings

        #region Folder Mnanager Settings

        [DataMember]
        public static readonly int FirstFolderId = 1;
        [DataMember]
        public static readonly string XmlFilename = @"folder_manager.xml";
        [DataMember]
        public static readonly string XmlPath = Path.Combine(SaveFolderPath, XmlFilename);
        [DataMember]
        private int nextID;

        public int NextID
        {
            get { return nextID++; }
        }

        #endregion Folder Mnanager Settings

        #region Singleton

        private static FolderManager singleton;

        public static FolderManager GetInstance()
        {
            if (singleton == null)
                Load();
            return singleton;
        }

        #endregion Singleton

        #region Folders Data

        #endregion Folders Data

        #region Constructors

        private FolderManager()
        {
            ResetDirectory();
            ResetID();
        }

        private void ResetDirectory()
        {
            if (!Directory.Exists(SaveFolderPath))
            {
                // if it doesn't exist, create
                Directory.CreateDirectory(SaveFolderPath);
            }

            if (!Directory.Exists(DataFolderPath))
                Directory.CreateDirectory(DataFolderPath);
        }

        private void ResetID()
        {
            nextID = FirstFolderId;
        }

        #endregion Constructors

        #region Operations

        public void Save()
        {
            var serializer = new DataContractSerializer(typeof(FolderManager));
            using (var file = File.Create(XmlPath))
                serializer.WriteObject(file, singleton);
        }

        private static void Load()
        {
            if (File.Exists(XmlPath))
            {
                using (var file = File.OpenRead(XmlPath))
                {
                    try
                    {
                        var deserializer = new DataContractSerializer(typeof(FolderManager));
                        using (var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas()))
                            singleton = (FolderManager)deserializer.ReadObject(reader, true);
                    }
                    catch (SerializationException)
                    {
                        singleton = new FolderManager();
                    }
                }
            }
            else
            {
                singleton = new FolderManager();
            }
        }

        #endregion Operations
    }
}