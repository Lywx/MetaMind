namespace MetaMind.Engine.Entities
{
    public interface IMMMvcEntity : IMMInputEntity
    {
        IMMMvcEntityLogic Logic { get; }

        IMMMvcEntityVisual Visual { get; }
    }
}