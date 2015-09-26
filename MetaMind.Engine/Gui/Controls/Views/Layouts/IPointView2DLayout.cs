namespace MetaMind.Engine.Gui.Controls.Views.Layouts
{
    public interface IPointView2DLayout : IPointViewHorizontalLayout, IPointViewVerticalLayout
    {
        int IdFrom(int i, int j);
    }
}