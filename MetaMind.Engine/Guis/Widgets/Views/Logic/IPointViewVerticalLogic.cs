namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IPointViewVerticalLogic : IViewLogic
    {
        new IPointViewVerticalSwapController ViewSwap { get; } 

        new IPointViewVerticalSelectionController ViewSelection { get; }

        new IPointViewVerticalScrollController ViewScroll { get; }
    }
}