namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Sprache;

    public interface IViewSelectionSupport : IViewComponent, IInput
    {
        dynamic ViewSelection { get; }
    }
}