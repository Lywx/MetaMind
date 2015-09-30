namespace MetaMind.Engine.Gui.Modules
{
    public interface IMMMvcEntity : IMMInputableEntity
    {
        IGameMvcEntityLogic Logic { get; }

        IMMMvcEntityVisual Visual { get; }
    }
}