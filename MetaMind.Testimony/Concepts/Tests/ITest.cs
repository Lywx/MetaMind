namespace MetaMind.Testimony.Concepts.Tests
{
    public interface ITest : ITestStructure, IInnerUpdatable
    {
        string Name { get; set; }

        string Description { get; set; }

        string Status { get; set; }
    }
}