namespace MetaMind.Runtime.Concepts.Synchronizations
{
    using System.Runtime.Serialization;

    public interface ISynchronizable
    {
        string SynchronizationName { get; }

        ISynchronizationData SynchronizationData { get; set; }
    }

    public interface ISynchronizationData
    {
        #region Transition Data

        bool IsSynchronizing { get; set; }

        #endregion

        #region Time Data

        SynchronizationSpan SynchronizationSpan { get; set; }

        #endregion
    }

    [DataContract]
    public class SynchronizationData : ISynchronizationData
    {
        public SynchronizationData()
        {
            this.SynchronizationSpan = SynchronizationSpan.Zero;

            this.IsSynchronizing = false;
        }

        #region Transition Data

        [DataMember]
        public bool IsSynchronizing { get; set; }

        #endregion

        #region Time Data

        [DataMember]
        public SynchronizationSpan SynchronizationSpan { get; set; }

        #endregion
    }
}