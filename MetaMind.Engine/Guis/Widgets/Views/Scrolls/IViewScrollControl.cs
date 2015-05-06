namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Microsoft.Xna.Framework;

    public interface IViewScrollControl : IViewComponent
    {
        bool CanDisplay(int id);

        Vector2 Position(int id); 

        void Zoom(int id);
    }
}