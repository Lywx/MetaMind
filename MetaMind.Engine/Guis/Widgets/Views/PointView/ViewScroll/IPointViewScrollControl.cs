namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll
{
    using Microsoft.Xna.Framework;

    public interface IPointViewScrollControl
    {
        bool CanDisplay(int id);

        Point RootCenterPosition(int id); 

        void Zoom(int id);
    }
}