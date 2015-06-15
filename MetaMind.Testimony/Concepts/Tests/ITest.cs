namespace MetaMind.Testimony.Concepts.Tests
{
    using System;

    public interface ITest : ITestStructure, ITestComputation, ITestOperations,
        IComparable<ITest>, IEquatable<ITest>,
        IInnerUpdatable
    {
        #region Properties

        string Name { get; }

        string Description { get; }
        
        string Status { get; }

        string Path { get; }

        bool Passed { get; }

        #endregion

        #region Events

        event EventHandler Succeeding;

        event EventHandler Failing;

        #endregion
    }
}