namespace MetaMind.Engine.Guis
{
    public interface IModule : IInputable, IDrawable, IGameControllableEntity
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }
    }
}