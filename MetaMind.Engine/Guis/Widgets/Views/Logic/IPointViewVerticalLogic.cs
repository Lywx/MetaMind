namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IPointViewVerticalLogic : IViewLogic
    {
        new IPointViewVerticalLayout ViewLayout { get; }

        new IPointViewVerticalSwapController ViewSwap { get; } 

        new IPointViewVerticalSelectionController ViewSelection { get; }

        new IPointViewVerticalScrollController ViewScroll { get; }
    }
}