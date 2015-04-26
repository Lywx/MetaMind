namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public interface IPointViewScrollControl
    {
        Point RootCenterPoint(int id); 

        void Zoom(int id);
        
        bool CanDisplay(int id);
    }
}