namespace MetaMind.Testimony.Concepts.Tests
{
    using System;

    public interface ITest : ITestStructure, ITestComputation, ITestOperations, IInnerUpdatable
    {
        #region Properties

        string Name { get; }

        string Description { get; }

        string Status { get; }

        bool Passed { get; }

        #endregion

        #region Events

        event EventHandler Succeeding;

        event EventHandler Failing;

        #endregion
    }
}