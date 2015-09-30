namespace MetaMind.Engine.Gui.Controls.Views
{
    using Layers;
    using Reactors;

    public interface IViewComponent : IGameReactor, IGameInputableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}