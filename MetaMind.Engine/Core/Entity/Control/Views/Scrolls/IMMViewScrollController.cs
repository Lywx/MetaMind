namespace MetaMind.Engine.Core.Entity.Control.Views.Scrolls
{
    using Microsoft.Xna.Framework;

    public interface IMMViewScrollController : IMMViewComponent
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