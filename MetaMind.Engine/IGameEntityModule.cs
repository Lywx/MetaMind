namespace MetaMind.Engine
{
    public interface IGameEntityModule : IGameControllableEntity
    {
        IGameEntityModuleLogic Logic { get; }

        IGameEntityModuleVisual Visual { get; }
    }
}