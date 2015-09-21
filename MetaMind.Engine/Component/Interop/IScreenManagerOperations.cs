namespace MetaMind.Engine.Component.Interop
{
    using Microsoft.Xna.Framework;
    using Screen;

    public interface IScreenManagerOperations
    {
        #region Operations

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        void AddScreen(IGameScreen screen);

        /// <summary>
        /// Exits all screen at and after the index.
        /// </summary>
        void EraseScreenFrom(int index);

        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        void FadeScreen(float alpha, Color color);

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use Screen.Exit instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        void RemoveScreen(IGameScreen screen);

        #endregion

        #region Events

        void OnExiting();

        #endregion
    }
}