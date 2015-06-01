namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Microsoft.Xna.Framework;

    public interface IViewScrollController : IViewComponent
    {
        bool CanDisplay(int id);

        Vector2 Position(int id); 

        void Zoom(int id);
    }
}