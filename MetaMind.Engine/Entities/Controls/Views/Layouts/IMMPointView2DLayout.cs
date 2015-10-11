namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    public interface IMMPointView2DLayout : IMMPointViewHorizontalLayout, IMMPointViewVerticalLayout
    {
        int IdFrom(int i, int j);
    }
}