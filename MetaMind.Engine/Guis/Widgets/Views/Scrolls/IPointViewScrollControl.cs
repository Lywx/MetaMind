namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Microsoft.Xna.Framework;

    public interface IPointViewScrollControl : IViewSrollControl
    {
        bool CanDisplay(int id);

        Point RootCenterPosition(int id); 

        void Zoom(int id);
    }
}