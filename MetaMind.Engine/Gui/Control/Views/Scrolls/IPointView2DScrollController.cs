namespace MetaMind.Engine.Gui.Control.Views.Scrolls
{
    public interface IPointView2DScrollController : IPointViewHorizontalScrollController, IPointViewVerticalScrollController
    {
        bool CanDisplay(int row, int column);
    }
}