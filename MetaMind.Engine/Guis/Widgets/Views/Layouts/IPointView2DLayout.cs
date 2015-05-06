namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    public interface IPointView2DLayout : IPointViewHorizontalLayout
    {
        int RowNum { get; }

        int IdFrom(int i, int j);

        int RowFrom(int id);
    }
}