using MetaMind.Engine;
using MetaMind.Engine.Concepts;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [DataContract]
    public class TaskEntry : EngineObject
    {
        #region Name

        [DataMember]
        public string Name = string.Empty;

        #endregion Name

        #region Experience

        [DataMember]
        public Experience Experience;

        [DataMember]
        public bool Synchronizing;

        #endregion Experience

        #region Error

        [DataMember]
        public bool Rationalized;

        #endregion Error

        #region Progress

        [DataMember]
        public int Done;

        [DataMember]
        public int Load;

        #endregion Progress

        public TaskEntry()
        {
            Experience = Experience.Zero;
        }
    }
}