namespace MetaMind.Engine.Gui.Controls.Views
{
    using Entities;
    using Layers;

    public interface IViewComponent : IMMReactor, IMMInputEntity, IMMViewComponentOperations, IViewLayerOperations
    {
        IMMViewNode View { get; }
    }
}