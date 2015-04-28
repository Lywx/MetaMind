namespace MetaMind.Engine.Guis
{
    public interface IModule : IGameControllableEntity
    {
        IModuleLogicControl Logic { get; }

        IModuleVisualControl Visual { get; }
    }
}