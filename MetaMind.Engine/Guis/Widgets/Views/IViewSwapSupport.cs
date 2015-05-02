namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Sprache;

    public interface IViewSwapSupport : IViewComponent, IInput
    {
        dynamic ViewSwap { get; }
    }
}