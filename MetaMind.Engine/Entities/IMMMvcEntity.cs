namespace MetaMind.Engine.Entities
{
    public interface IMMMvcEntity : IMMInputableEntity
    {
        IMMMvcEntityLogic Logic { get; }

        IMMMvcEntityVisual Visual { get; }
    }
}