namespace MetaMind.Testimony.Concepts.Synchronizations
{
    using System.Runtime.Serialization;

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