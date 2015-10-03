namespace MetaMind.Engine.Gui.Controls.Views
{
    using Entities;
    using Layers;

    public interface IViewComponent : IMMReactor, IMMInputEntity, IViewComponentOperations, IViewLayerOperations
    {
        IMMViewNode View { get; }
    }
}