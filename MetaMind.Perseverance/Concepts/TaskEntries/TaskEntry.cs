namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    using System.Runtime.Serialization;

    using MetaMind.Engine;
    using MetaMind.Engine.Concepts;

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