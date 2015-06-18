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

        int PassedChange { get; }

        bool PassedChanged { get; }

        #endregion

        #region Events

        event EventHandler Succeed;

        event EventHandler Fail;

        #endregion
    }
}