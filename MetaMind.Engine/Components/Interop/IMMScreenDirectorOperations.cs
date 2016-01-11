namespace MetaMind.Engine.Components.Interop
{
    using Entities.Screens;

    public interface IMMScreenDirectorOperations
    {
        #region Operations

        /// <summary>
        /// Adds a new screen to the screen director.
        /// </summary>
        void AddScreen(IMMScreen screen);

        /// <summary>
        /// Exists given screen.
        /// </summary>
        /// <param name="screen"></param>
        void ExitScreen(IMMScreen screen);

        /// <summary>
        /// Exits all screen at and after the index.
        /// </summary>
        void ExitScreenFrom(int index);

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use Screen.Exit() or Director.ExitScreen() instead of calling this 
        /// directly, so the screen can gradually transition off rather than 
        /// just being instantly removed.
        /// </summary>
        void RemoveScreen(IMMScreen screen);

        /// <summary>
        /// Removes current screen at the top of the stack with the given screen.
        /// </summary>
        void ReplaceScreen(IMMScreen screen);

        #endregion

        #region Events

        void OnExiting();

        #endregion
    }
}