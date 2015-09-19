namespace MetaMind.Engine.Screen
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IGameScreenOperations : IInputableOperations, IOuterUpdateableOperations
    {
        #region Draw

        void BeginDraw(IGameGraphicsService graphics, GameTime time);

        void EndDraw(IGameGraphicsService graphics, GameTime time);

        #endregion

        #region Update

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike other update method, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        void UpdateScreen(IGameInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen);

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