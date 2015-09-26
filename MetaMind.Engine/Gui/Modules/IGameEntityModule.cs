namespace MetaMind.Engine.Gui.Modules
{
    public interface IGameEntityModule : IGameControllableEntity
    {
        IGameEntityModuleLogic Logic { get; }

        IGameEntityModuleVisual Visual { get; }
    }
}