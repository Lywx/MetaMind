namespace MetaMind.Unity.Concepts.Tests
{
    using System.Collections;
    using System.Collections.Generic;

    public interface ITestOrganization
    {
        List<ITest> Children { get; }

        Test Parent { get; set; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable AllCollection();

        IEnumerable ChildrenCollection();
    }
}