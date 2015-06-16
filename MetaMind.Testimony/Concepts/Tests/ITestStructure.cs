namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Engine.Collections;

    public interface ITestStructure
    {
        List<ITest> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable AllTests();

        IEnumerable ChildrenTests();
    }
}