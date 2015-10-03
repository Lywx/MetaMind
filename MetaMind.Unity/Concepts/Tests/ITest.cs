namespace MetaMind.Unity.Concepts.Tests
{
    using System;
    using System.Collections.Generic;
    using Engine;
    using Events;

    public interface ITestOrganization : IMMFreeUpdatable, ITestOperations
    {
        List<ITest> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable<ITest> AllCollection { get; }

        IEnumerable<ITest> ChildrenCollection { get; }
    }

    public interface ITestOperations
    {
        void Reset();
    }

    public interface ITest : 
        ITestOperations,
        ITestOrganization,

        IComparable<ITest>, 
        IMMFreeUpdatable
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