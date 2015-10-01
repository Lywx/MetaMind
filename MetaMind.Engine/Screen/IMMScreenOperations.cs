namespace MetaMind.Engine.Screen
{
    using Entities;
    using Microsoft.Xna.Framework;
    using Service;

    public interface IMMScreenOperations : IMMInputOperations, IMMUpdateableOperations
    {
        #region Draw

        void BeginDraw(IMMEngineGraphicsService graphics, GameTime time);

        void EndDraw(IMMEngineGraphicsService graphics, GameTime time);

        #endregion

        #region Update

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike other update method, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        void UpdateScreen(IMMEngineInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen);

        #endregion Update

        #region Operations

        /// <summary>
        /// Tells the screen to go away. Unlike Screens.RemoveScreen, which
        /// instantly kills the screen, this method respects the transition timings
        /// and will give the screen a chance to gradually transition off.
        /// </summary>
        void Exit();

        #endregion 
    }
}