namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    public interface IPointView2DScrollControl : IPointViewHorizontalScrollControl, IPointViewVerticalScrollControl
    {
        bool CanDisplay(int row, int column);
    }
}