namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewComponent : IGameControllableEntity, IViewComponentOperations
    {
        IView View { get; }
    }
}