namespace MetaMind.Engine.Core.Backend.Input.Keyboard
{
    using Microsoft.Xna.Framework.Input;

    public interface IMMKeyboardInput 
    {
        #region Action States

        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        bool IsActionPressed(MMInputAction action);

        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        bool IsActionTriggered(MMInputAction action);

        #endregion

        #region Key States

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        bool IsKeyPressed(Keys key);

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        bool IsKeyTriggered(Keys key);

        #endregion

        #region Modifier States

        bool IsAltDown { get; }

        bool IsCtrlDown { get; }

        bool IsShiftDown { get; }

        #endregion

        #region Operations

        /// <summary>
        /// Call this method to load action bindings from configuration file.
        /// </summary>
        /// <typeparam name="TActions"></typeparam>
        void LoadActions<TActions>() where TActions : MMInputActions;

        #endregion
    }
}