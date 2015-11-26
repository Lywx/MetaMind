namespace MetaMind.Session.Concepts.Tests
{
    using System.Collections.Generic;
    using Engine;

    public interface ITestOrganization : IMMFreeUpdatable, ITestOperations
    {
        List<ITest> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable<ITest> AllCollection { get; }

        IEnumerable<ITest> ChildrenCollection { get; }
    }
}