namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Layers;

    public interface IViewComponent : IGameControllableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}