namespace MetaMind.Engine.Gui.Controls.Views
{
    using Components;
    using Layers;

    public interface IViewComponent : IComponent, IGameInputableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}