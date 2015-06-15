namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections;
    using Engine.Collections;

    public interface ITestStructure
    {
        ObservableCollection<ITest> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable AllTests();

        IEnumerable ChildrenTests();
    }
}