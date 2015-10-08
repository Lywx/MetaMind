namespace MetaMind.Session.Concepts.Synchronizations
{
    public interface ISynchronizationData
    {
        #region Transition Data

        bool IsSynchronizing { get; set; }

        #endregion

        #region Time Data

        SynchronizationSpan SynchronizationSpan { get; set; }

        #endregion
    }
}