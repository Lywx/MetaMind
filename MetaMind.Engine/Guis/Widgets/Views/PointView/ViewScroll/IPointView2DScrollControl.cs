namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll
{
    public interface IPointView2DScrollControl : IPointViewHorizontalScrollControl, IPointViewVerticalScrollControl
    {
        bool CanDisplay(int row, int column);
    }
}