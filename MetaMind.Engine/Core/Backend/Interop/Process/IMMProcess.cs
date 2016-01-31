namespace MetaMind.Engine.Core.Backend.Interop.Process
{
    using System;
    using Entity.Common;

    public interface IMMProcessOperations
    {
        void Abort();

        void AttachChild(IMMProcess process);

        void Fail();

        void OnAbort();

        void OnFail();

        void OnInit();

        void OnSucceed();

        void Pause();

        IMMProcess RemoveChild();

        void Resume();

        void Succeed();
    }

    public interface IMMProcessBase : IMMUpdateable, IDisposable 
    {
    }

    public interface IMMProcess : IMMProcessBase, IMMProcessOperations
    {
        string Name { get; set; }

        MMProcessCategory Category { get; }

        IMMProcess Child { get; }

        bool IsAlive { get; }

        bool IsDead { get; }

        bool IsPaused { get; }

        bool IsRemoved { get; }

        ProcessState State { get; }
    }
}