namespace MetaMind.Unity.Concepts.Tests
{
    using System;

    using Engine;

    public interface ITestEvaluation : IInnerUpdatable, ITestOperations, IDisposable
    {
        string ResultStatus { get; }

        Func<string> ResultStatusSelector { set; }

        bool ResultPassed { get; }

        bool ResultChanged { get; }

        int ResultChildrenPassed { get; }

        int ResultAllPassed { get; }

        float ResultAllPassedRate { get; }

        int ResultChange { get; }

        Func<bool> ResultSelector { set; }

        #region Events

        event EventHandler<TestEventArgs> Succeeded;

        event EventHandler<TestEventArgs> Failed;

        void Succeed(bool isCause);

        void Fail(bool isCause);

        #endregion

        void UpdateResult();
    }
}