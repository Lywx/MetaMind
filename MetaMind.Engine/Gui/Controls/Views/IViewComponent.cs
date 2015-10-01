namespace MetaMind.Engine.Gui.Controls.Views
{
    using Entities;
    using Layers;
    using Reactors;

    public interface IViewComponent : IMMReactor, IMMInputableEntity, IViewComponentOperations, IViewLayerOperations
    {
        IView View { get; }
    }
}