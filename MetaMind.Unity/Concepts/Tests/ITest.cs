namespace MetaMind.Unity.Concepts.Tests
{
    using System;
    using Engine;

    public interface ITest : 
        ITestOperations,
        ITestOrganization,

        IComparable<ITest>, 

        IInnerUpdatable
    {
        #region Properties

        string Name { get; }

        string Description { get; }
        
        string Path { get; }

        ITestEvaluation Evaluation { get; }

        ITestOrganization Organization { get; }

        #endregion

        #region Events

        event EventHandler<TestEventArgs> Succeeded;

        event EventHandler<TestEventArgs> Failed;

        #endregion
    }
}