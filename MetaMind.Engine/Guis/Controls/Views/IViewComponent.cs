namespace MetaMind.Engine.Guis.Controls.Views
{
    using Layers;

    public interface IViewComponent : IGameControllableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}