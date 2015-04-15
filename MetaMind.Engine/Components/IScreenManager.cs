namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Screens;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IScreenManager
    {
        GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        IGameScreen[] Screens { get; }

        ScreenSettings Settings { get; }

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        SpriteBatch SpriteBatch { get; }

        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        bool TraceEnabled { get; set; }

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        void AddScreen(IGameScreen screen);

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
    }
}