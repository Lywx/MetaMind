namespace MetaMind.Engine.Guis
{
    public interface IModule : IGameControllableEntity
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }
    }
}