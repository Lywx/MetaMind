using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components.Inputs
{
    public class KeyboardManager
    {
        #region Action Mappings

        /// <summary>
        /// Readable names of each action.
        /// </summary>
        private static readonly string[] actionNames =
            {
                "Move Cursor - Up", // w
                "Move Cursor - Down", // s
                "Move Cursor - Left", // disabled
                "Move Cursor - Right", // disabled

                "Add Tile", // o
                "Delete Tile", // g

                "Edit Tile Name", // n

                "Shift", // shift
                "Enter Input", // enter
                "Exit" // esc
            };

        /// <summary>
        /// The action mappings for the game.
        /// The action system hierarchy: Action - ActionMap - Keys
        /// </summary>
        private static ActionMap[] actionMaps;

        public static ActionMap[ ] ActionMaps
        {
            get { return actionMaps; }
        }

        /// <summary>
        /// Returns the readable name of the given action.
        /// </summary>
        public static string GetActionName( Actions action )
        {
            int index = ( int ) action;

            if ( ( index < 0 ) || ( index > actionNames.Length ) )
            {
                throw new ArgumentException( "Action Enum Error." );
            }

            return actionNames[ index ];
        }

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
        /// Reset the action maps to their default values.
        /// </summary>
        private static void ResetActionMaps()
        {
            actionMaps = new ActionMap[ ( int ) Actions.ActionNum ];

            // cursor movement
            actionMaps[ ( int ) Actions.Up ] = new ActionMap();
            actionMaps[ ( int ) Actions.Up ].Keys.Add(
                Keys.W );

            actionMaps[ ( int ) Actions.Down ] = new ActionMap();
            actionMaps[ ( int ) Actions.Down ].Keys.Add(
                Keys.S );

            actionMaps[ ( int ) Actions.Left ] = new ActionMap();
            actionMaps[ ( int ) Actions.Left ].Keys.Add(
                Keys.A );

            actionMaps[ ( int ) Actions.Right ] = new ActionMap();
            actionMaps[ ( int ) Actions.Right ].Keys.Add(
                Keys.D );

            // basic ui commands
            actionMaps[ ( int ) Actions.Esc ] = new ActionMap();
            actionMaps[ ( int ) Actions.Esc ].Keys.Add(
                Keys.Escape );

            actionMaps[ ( int ) Actions.Enter ] = new ActionMap();
            actionMaps[ ( int ) Actions.Enter ].Keys.Add(
                Keys.Enter );

            actionMaps[ ( int ) Actions.LeftControl ] = new ActionMap();
            actionMaps[ ( int ) Actions.LeftControl ].Keys.Add(
                Keys.LeftControl );

            // tile management
            actionMaps[ ( int ) Actions.NewItem ] = new ActionMap();
            actionMaps[ ( int ) Actions.NewItem ].Keys.Add(
                Keys.N );

            actionMaps[ ( int ) Actions.DeleteItem ] = new ActionMap();
            actionMaps[ ( int ) Actions.DeleteItem ].Keys.Add(
                Keys.Delete );

            actionMaps[ ( int ) Actions.NewChildItem ] = new ActionMap();
            actionMaps[ ( int ) Actions.NewChildItem ].Keys.Add(
                Keys.C );

            // tile edit
            actionMaps[ ( int ) Actions.EditTileName ] = new ActionMap();
            actionMaps[ ( int ) Actions.EditTileName ].Keys.Add(
                Keys.F2 );

            // concept 1
            actionMaps[ ( int ) Actions.PullItem ] = new ActionMap();
            actionMaps[ ( int ) Actions.PullItem ].Keys.Add(
                Keys.OemPeriod );

            actionMaps[ ( int ) Actions.StretchItem ] = new ActionMap();
            actionMaps[ ( int ) Actions.StretchItem ].Keys.Add(
                Keys.OemComma );

            // concept 2
            actionMaps[ ( int ) Actions.FinishItem ] = new ActionMap();
            actionMaps[ ( int ) Actions.FinishItem ].Keys.Add(
                Keys.F );
        }

        /// <summary>
        /// Check if an action map has been pressed.
        /// </summary>
        private bool IsActionMapPressed( ActionMap actionMap )
        {
            return actionMap.Keys.Any( IsKeyPressed );
        }

        /// <summary>
        /// Check if an action map has been triggered this frame.
        /// </summary>
        private bool IsActionMapTriggered( ActionMap actionMap )
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

        #endregion Action Mappings

        #region Input Data

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

        #endregion Input Data

        #region Constructors

        private KeyboardManager()
        {
        }

        public void Initialize()
        {
            ResetActionMaps();
        }

        #endregion Constructors

        #region Input Triggers

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

        #endregion Input Triggers

        #region Singleton

        private static KeyboardManager singleton;

        public static KeyboardManager GetInstance()
        {
            return singleton ?? ( singleton = new KeyboardManager() );
        }

        #endregion Singleton

        #region Update

        public void HandleInput()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        #endregion Update
    }
}