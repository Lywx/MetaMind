namespace MetaMind.Session.Concepts.Tests
{
    using System;
    using Engine;

    public interface ITestOperations
    {
        void Reset();
    }

    public interface ITestBase : IMMFreeUpdatable, IComparable<ITest>
    {
        
    }

    public interface ITest : ITestBase, ITestOperations, ITestOrganization
    {
        #region Properties

        string Name { get; }

        string Description { get; }
        
        string Path { get; }

        ITestEvaluation Evaluation { get; }

        ITestOrganization Organization { get; }

        #endregion

        #region Events

        event EventHandler<TestEvaluationEventArgs> Succeeded;

        event EventHandler<TestEvaluationEventArgs> Failed;

        #endregion
    }
}