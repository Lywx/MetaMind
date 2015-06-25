namespace MetaMind.Testimony.Concepts.Tests
{
    using System;

    public interface ITest : 
        ITestComputation,
        ITestOrganization,
        ITestOperations,

        IComparable<ITest>, 
        IEquatable<ITest>,

        IInnerUpdatable
    {
        #region Properties

        string Name { get; }

        string Description { get; }
        
        string Path { get; }

        #endregion

        #region Events

        event EventHandler Succeed;

        event EventHandler Fail;

        #endregion
    }
}