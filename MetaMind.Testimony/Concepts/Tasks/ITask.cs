namespace MetaMind.Testimony.Concepts.Tasks
{
    using Synchronizations;

    public interface ITask : ISynchronizable
    {
        string Name { get; set; }
    }
}