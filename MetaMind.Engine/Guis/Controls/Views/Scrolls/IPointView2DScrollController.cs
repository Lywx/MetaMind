namespace MetaMind.Engine.Guis.Controls.Views.Scrolls
{
    public interface IPointView2DScrollController : IPointViewHorizontalScrollController, IPointViewVerticalScrollController
    {
        bool CanDisplay(int row, int column);
    }
}