namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewScrollControl2D : IPointViewScrollControlHorizontal, IPointViewScrollControlVertical
    {
        bool CanDisplay(int row, int column);
    }
}