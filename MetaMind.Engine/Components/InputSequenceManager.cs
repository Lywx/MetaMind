using MetaMind.Engine.Components.Inputs;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputSequenceManager
    {
        #region Manager Data

        private bool isInitialized = false;

        #endregion Manager Data

        #region Components

        private KeyboardManager keyboard;

        private MouseManager mouse;

        public KeyboardManager Keyboard
        {
            get { return keyboard; }
        }

        public MouseManager Mouse
        {
            get { return mouse; }
        }

        #endregion Components

        #region Constructors

        private InputSequenceManager(Game game)
        {
            keyboard = KeyboardManager.GetInstance();
            mouse = MouseManager.GetInstance();

            if (!isInitialized)
                Initialize();
        }

        #endregion Constructors

        #region Initialization

        /// <summary>
        /// Allows the game component to perform any Constructors it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            keyboard.Initialize();
            mouse.Initialize();
        }

        #endregion Initialization

        #region Singleton

        private static InputSequenceManager singleton;

        public static InputSequenceManager GetInstance(Game game)
        {
            return singleton ?? (singleton = new InputSequenceManager(game));
        }

        #endregion Singleton

        #region Update

        public void HandleInput()
        {
            mouse.HandleInput();
            keyboard.HandleInput();
        }

        #endregion Update
    }
}