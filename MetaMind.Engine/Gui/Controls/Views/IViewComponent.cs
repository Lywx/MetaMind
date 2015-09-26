namespace MetaMind.Engine.Gui.Controls.Views
{
    using Layers;

    public interface IViewComponent : IGameControllableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}