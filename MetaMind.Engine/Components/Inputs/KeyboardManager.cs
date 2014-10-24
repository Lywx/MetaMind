using System.Collections.Generic;
using System.Linq;
using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components.Inputs
{
    /// <summary>
    /// The actions that are possible within the game.
    /// </summary>
    public enum Actions
    {
        //---------------------------------------------------------------------
        // screen movement
        NextScreen,

        LastScreen,

        //---------------------------------------------------------------------
        // cursor movement
        Up,

        Down,
        Left,
        Right,

        //---------------------------------------------------------------------
        // list management
        CreateItem,

        CreateChildItem,
        EditItem,

        //---------------------------------------------------------------------
        // general
        Enter,

        Esc,

        //---------------------------------------------------------------------
        ActionNum
    }

    public class KeyboardActionMap
    {
        /// <summary>
        /// List of Keyboard controls to be mapped to a given action.
        /// </summary>
        public List<Keys> Keys = new List<Keys>();
    }

    public class KeyboardManager : InputObject
    {
        #region Singleton

        private static KeyboardManager singleton;

        public static KeyboardManager GetInstance()
        {
            return singleton ?? ( singleton = new KeyboardManager() );
        }

        #endregion Singleton

        #region Latch State

        private KeyboardState currentState;
        private KeyboardState previousState;

        public KeyboardState CurrentState
        {
            get { return currentState; }
        }

        public KeyboardState PreviousState
        {
            get { return previousState; }
        }

        #endregion Latch State

        #region Modifier State

        public bool AltDown
        {
            get
            {
                var state = Keyboard.GetState();
                return state.IsKeyDown( Keys.LeftAlt ) || state.IsKeyDown( Keys.RightAlt );
            }
        }

        public bool CtrlDown
        {
            get
            {
                var state = Keyboard.GetState();
                return state.IsKeyDown( Keys.LeftControl ) || state.IsKeyDown( Keys.RightControl );
            }
        }

        public bool ShiftDown
        {
            get
            {
                var state = Keyboard.GetState();
                return state.IsKeyDown( Keys.LeftShift ) || state.IsKeyDown( Keys.RightShift );
            }
        }

        #endregion Modifier State

        #region Action States

        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        public bool IsActionPressed( Actions action )
        {
            return IsActionMapPressed( actionMaps[ ( int ) action ] );
        }

        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        public bool IsActionTriggered( Actions action )
        {
            return IsActionMapTriggered( actionMaps[ ( int ) action ] );
        }

        /// <summary>
        /// Check if an action map has been pressed.
        /// </summary>
        private bool IsActionMapPressed( KeyboardActionMap actionMap )
        {
            return actionMap.Keys.Any( IsKeyPressed );
        }

        /// <summary>
        /// Check if an action map has been triggered this frame.
        /// </summary>
        private bool IsActionMapTriggered( KeyboardActionMap actionMap )
        {
            for ( var i = 0 ; i < actionMap.Keys.Count ; i++ )
            {
                if ( IsKeyTriggered( actionMap.Keys[ i ] ) )
                {
                    return true;
                }
            }
            return false;
        }

        #endregion Action States

        #region Key States

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        public bool IsKeyPressed( Keys key )
        {
            return currentState.IsKeyDown( key );
        }

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public bool IsKeyTriggered( Keys key )
        {
            return currentState.IsKeyDown( key ) &&
                   !previousState.IsKeyDown( key );
        }

        #endregion Key States

        #region Constructors

        private KeyboardManager()
        {
            ResetActionMaps();
        }

        #endregion Constructors

        #region Keyboard Action Mappings

        /// <summary>
        /// The action mappings for the game.
        /// The action system hierarchy: Action - KeyboardActionMap - Keys
        /// </summary>
        private static KeyboardActionMap[] actionMaps;

        public static KeyboardActionMap[ ] ActionMaps
        {
            get { return actionMaps; }
        }

        /// <summary>
        /// Reset the action maps to their default values.
        /// </summary>
        private static void ResetActionMaps()
        {
            actionMaps = new KeyboardActionMap[ ( int ) Actions.ActionNum ];

            //-----------------------------------------------------------------
            // cursor movement
            actionMaps[ ( int ) Actions.Up ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.Up ].Keys.Add(
                Keys.W );

            actionMaps[ ( int ) Actions.Down ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.Down ].Keys.Add(
                Keys.S );

            actionMaps[ ( int ) Actions.Left ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.Left ].Keys.Add(
                Keys.A );

            actionMaps[ ( int ) Actions.Right ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.Right ].Keys.Add(
                Keys.D );

            //-----------------------------------------------------------------
            // list management
            actionMaps[ ( int ) Actions.CreateItem ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.CreateItem ].Keys.Add(
                Keys.N );

            actionMaps[ ( int ) Actions.CreateChildItem ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.CreateChildItem ].Keys.Add(
                Keys.C );

            actionMaps[ ( int ) Actions.EditItem ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.EditItem ].Keys.Add(
                Keys.Delete );

            //-----------------------------------------------------------------
            // general
            actionMaps[ ( int ) Actions.Esc ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.Esc ].Keys.Add(
                Keys.Escape );

            actionMaps[ ( int ) Actions.Enter ] = new KeyboardActionMap();
            actionMaps[ ( int ) Actions.Enter ].Keys.Add(
                Keys.Enter );
        }

        #endregion Keyboard Action Mappings

        #region Update

        public override void Draw( GameTime gameTime )
        {
        }

        public override void UpdateInput( GameTime gameTime )
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        #endregion Update
    }
}