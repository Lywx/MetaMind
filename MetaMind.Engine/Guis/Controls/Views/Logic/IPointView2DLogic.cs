namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;

    public interface IPointView2DLogic : IPointViewVerticalLogic, IPointViewHorizontalLogic 
    {
        new IPointView2DLayout ViewLayout { get; }

        new IPointView2DSelectionController ViewSelection { get; }

        new IPointView2DScrollController ViewScroll { get; }
    }
}