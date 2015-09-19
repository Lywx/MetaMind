namespace MetaMind.Engine.Gui.Control.Views
{
    using Layers;

    public interface IViewComponent : IGameControllableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}