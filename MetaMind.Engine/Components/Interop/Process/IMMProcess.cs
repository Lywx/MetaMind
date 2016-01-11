namespace MetaMind.Engine.Components.Interop.Process
{
    using System;
    using Entities.Bases;

    public interface IMMProcess : IMMUpdateable, IDisposable
    {
        #region Process Data

        IMMProcess Child { get; }

        bool IsAlive { get; }

        bool IsDead { get; }

        bool IsPaused { get; }

        bool IsRemoved { get; }

        ProcessState State { get; }

        #endregion Process Data

        #region Transition

        void OnAbort();

        void OnFail();

        void OnInit();

        void OnSucceed();

        #endregion Transition

        #region Operations

        void Abort();

        void AttachChild(IMMProcess process);

        void Fail();

        void Pause();

        IMMProcess RemoveChild();

        void Resume();

        void Succeed();

        #endregion Operations
    }
}