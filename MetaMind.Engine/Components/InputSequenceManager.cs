using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputSequenceManager : InputObject
    {
        #region Singleton

        private static InputSequenceManager singleton;

        public static InputSequenceManager GetInstance()
        {
            return singleton ?? ( singleton = new InputSequenceManager() );
        }

        #endregion Singleton

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

        private InputSequenceManager()
        {
            keyboard = KeyboardManager.GetInstance();
            mouse = MouseManager.GetInstance();
        }

        #endregion Constructors

        #region Update

        public override void Draw( GameTime gameTime )
        {
        }

        public override void UpdateInput( GameTime gameTime )
        {
            mouse   .UpdateInput( gameTime );
            keyboard.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        #endregion Update
    }
}