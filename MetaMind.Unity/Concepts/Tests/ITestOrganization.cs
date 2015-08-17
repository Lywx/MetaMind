namespace MetaMind.Unity.Concepts.Tests
{
    using Engine;
    using System.Collections;
    using System.Collections.Generic;

    public interface ITestOrganization : IInnerUpdatable, ITestOperations
    {
        List<ITest> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable AllCollection { get; }

        IEnumerable ChildrenCollection { get; }
    }
}