namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    public interface IModule : IInputable, IDrawable
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }

        void LoadContent(IGameInteropService interop);

        void UnloadContent(IGameInteropService interop);
    }
}