namespace MetaMind.Session.Concepts.Tests
{
    using System;
    using Engine;

    public interface ITestEvaluation : IMMFreeUpdatable, ITestOperations, IDisposable
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

        event EventHandler<TestEvaluationEventArgs> Succeeded;

        event EventHandler<TestEvaluationEventArgs> Failed;

        void Succeed(bool isCause);

        void Fail(bool isCause);

        #endregion

        void UpdateResult();
    }
}