namespace MetaMind.Engine.Core.Entity.Control.Views.Layouts
{
    public interface IMMPointView2DLayout : IMMPointViewHorizontalLayout, IMMPointViewVerticalLayout
    {
        int IdFrom(int i, int j);
    }
}