namespace MetaMind.Engine.Gui.Control.Views.Logic
{
    using Layouts;
    using Scrolls;
    using Selections;

    public interface IPointView2DLogic : IPointViewVerticalLogic, IPointViewHorizontalLogic 
    {
        new IPointView2DLayout ViewLayout { get; }

        new IPointView2DSelectionController ViewSelection { get; }

        new IPointView2DScrollController ViewScroll { get; }
    }
}