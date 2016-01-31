namespace MetaMind.Engine.Core.Backend.Interop.Event
{
    public interface IMMEvent
    {
        #region Event Signature

        int Type { get; set; }

        object Data { get; set; }

        #endregion

        #region Time Data

        long CreationTime { get; set; }

        int LifeTime { get; set; }

        #endregion

        #region Handle Data

        bool Handled { get; set; }

        int HandleAttempts { get; set; }

        #endregion
    }
}