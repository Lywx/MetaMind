namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewScrollControl2D : IPointViewHorizontalScrollControl, IPointViewVerticalScrollControl
    {
        bool CanDisplay(int row, int column);
    }
}