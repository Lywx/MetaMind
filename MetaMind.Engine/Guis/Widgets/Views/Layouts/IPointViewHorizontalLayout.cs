namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    public interface IPointViewHorizontalLayout : IViewLayout
    {
        int ColumnNum { get; }

        int ColumnFrom(int id);
    }
}