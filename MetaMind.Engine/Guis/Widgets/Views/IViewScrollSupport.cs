namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Sprache;

    public interface IViewScrollSupport : IViewComponent, IInput
    {
        dynamic ViewScroll { get; }
    }
}