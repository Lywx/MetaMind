namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections;
    using System.Collections.ObjectModel;

    public interface ITestStructure
    {
        ObservableCollection<Test> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable AllTests();

        IEnumerable ChildrenTests();
    }
}