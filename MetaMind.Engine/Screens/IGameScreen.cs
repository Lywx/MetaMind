namespace MetaMind.Engine.Screens
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using System;

    public interface IGameScreen : IDisposable
    {
        #region Screen Option Data

        /// <summary>
        /// This property indicates whether the screen is only a small
        /// popup, in which case screens underneath it do not need to bother
        /// transitioning off.
        /// </summary>
        bool IsPopup { get; }

        /// <summary>
        /// Indicates how long the screen takes to
        /// transition off when it is deactivated.
        /// </summary>
        TimeSpan TransitionOffTime { get; }

        /// <summary>
        /// Indicates how long the screen takes to
        /// transition on when it is activated.
        /// </summary>
        TimeSpan TransitionOnTime { get; }

        #endregion Screen Option Data

        #region Screen State Data

        /// <summary>
        /// Checks whether this screen is active and can respond to user input.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// There are two possible reasons why a screen might be transitioning
        /// off. It could be temporarily going away to make room for another
        /// screen that is on top of it, or it could be going away for good.
        /// This property indicates whether the screen is exiting for real:
        /// if set, the screen will automatically remove itself as soon as the
        /// transition finishes.
        /// </summary>
        bool IsExiting { get; }

        /// <summary>
        /// Gets the current screen transition state.
        /// </summary>
        GameScreenState ScreenState { get; }

        /// <summary>
        /// Gets the current alpha of the screen transition, ranging
        /// from 255 (fully active, no transition) to 0 (transitioned
        /// fully off to nothing).
        /// </summary>
        byte TransitionAlpha { get; }

        /// <summary>
        /// Gets the current position of the screen transition, ranging
        /// from zero (fully active, no transition) to one (transitioned
        /// fully off to nothing).
        /// </summary>
        float TransitionPosition { get; }

        #endregion Screen State Data

        #region Screen Events

        event EventHandler Exiting;

        #endregion Screen Events

        #region Load and Unload

        void LoadContent(IGameInteropService interop);

        void UnloadContent(IGameInteropService interop);

        #endregion Load and Unload

        #region Update

        void Update(GameTime gameTime);
        
        void UpdateInput(IGameInputService input, GameTime time);

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike other update method, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        void UpdateScreen(Services.IGameInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen);

        #endregion Update

        #region Draw

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        void Draw(IGameGraphicsService graphics, GameTime time);

        #endregion Draw

        #region Operations

        /// <summary>
        /// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
        /// instantly kills the screen, this method respects the transition timings
        /// and will give the screen a chance to gradually transition off.
        /// </summary>
        void Exit();

        #endregion Operations
    }
}