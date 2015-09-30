namespace MetaMind.Engine.Gui.Modules
{
    public interface IGameMvcEntity : IGameInputableEntity
    {
        IGameMvcEntityLogic Logic { get; }

        IGameMvcEntityVisual Visual { get; }
    }
}