namespace MetaMind.Engine.Core.Entity.Control.Views.Scrolls
{
    public interface IMMPointView2DScrollController : IMMPointViewHorizontalScrollController, IMMPointViewVerticalScrollController
    {
        bool CanDisplay(int row, int column);
    }
}