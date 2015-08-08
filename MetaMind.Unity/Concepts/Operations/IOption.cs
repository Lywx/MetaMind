namespace MetaMind.Unity.Concepts.Operations
{
    /// <summary>
    /// Option is used as choices for trigger selection in operations.
    /// </summary>
    public interface IOption : IOptionOperation
    {
        string Name { get; }

        string Description { get; }
    }
}