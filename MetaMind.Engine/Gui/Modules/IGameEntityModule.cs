namespace MetaMind.Engine.Gui.Modules
{
    public interface IGameEntityModule : IGameInputableEntity
    {
        IGameEntityModuleLogic Logic { get; }

        IGameEntityModuleVisual Visual { get; }
    }
}