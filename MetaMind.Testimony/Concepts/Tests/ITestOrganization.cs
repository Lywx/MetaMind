namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections;
    using System.Collections.Generic;

    public interface ITestOrganization
    {
        List<ITest> Children { get; }

        Test Parent { get; set; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable AllTests();

        IEnumerable ChildrenTests();
    }
}