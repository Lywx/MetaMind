namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Microsoft.Xna.Framework;

    public interface IViewScrollController : IViewComponent
    {
        #region Display 

        bool CanDisplay(int id);

        Vector2 Position(int id);

        #endregion

        #region Operations

        void Zoom(int id);

        void Reset();

        #endregion
    }
}