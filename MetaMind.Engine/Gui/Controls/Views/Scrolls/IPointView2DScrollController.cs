namespace MetaMind.Engine.Gui.Controls.Views.Scrolls
{
    public interface IPointView2DScrollController : IPointViewHorizontalScrollController, IPointViewVerticalScrollController
    {
        bool CanDisplay(int row, int column);
    }
}