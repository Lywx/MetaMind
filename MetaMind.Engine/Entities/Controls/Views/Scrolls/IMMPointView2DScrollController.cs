namespace MetaMind.Engine.Entities.Controls.Views.Scrolls
{
    public interface IMMPointView2DScrollController : IMMPointViewHorizontalScrollController, IMMPointViewVerticalScrollController
    {
        bool CanDisplay(int row, int column);
    }
}