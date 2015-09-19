namespace MetaMind.Engine.Component.Input
{
    using Microsoft.Xna.Framework.Input;

    public interface IKeyboardInputState
    {
        KeyboardState CurrentState { get; }

        KeyboardState PreviousState { get; }

        bool AltDown { get; }

        bool CtrlDown { get; }

        bool ShiftDown { get; }

        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        bool IsActionPressed(KeyboardActions action);

        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        bool IsActionTriggered(KeyboardActions action);

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        bool IsKeyPressed(Keys key);

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        bool IsKeyTriggered(Keys key);
    }
}