// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using System.Collections.Generic;
    using System.Linq;

    using MetaMind.Engine.Guis.Widgets;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// The actions that are possible within the game.
    /// </summary>
    public enum Actions
    {
        // cursor movement
        // ---------------------------------------------------------------------
        Up, 

        Down, 

        Left, 

        Right, 

        SUp, 

        SDown, 

        SLeft, 

        SRight, 

        // list management
        // ---------------------------------------------------------------------
        MotivationCreateItem, 

        MotivationDeleteItem, 

        MotivationEditItem, 

        TaskCreateItem, 

        TaskDeleteItem, 

        TaskEditItem, 

        // synchronization
        // ---------------------------------------------------------------------
        ForceReset,

        // general
        // ---------------------------------------------------------------------
        Enter, 

        Escape, 

        // ---------------------------------------------------------------------
        ActionNum, 
    }

    public class KeyboardActionMap
    {
        /// <summary>
        /// Dictionary of key, modifier pair to be mapped to a given action.
        /// </summary>
        public Dictionary<Keys, List<Keys>> Bindings = new Dictionary<Keys, List<Keys>>();
    }

    public class KeyboardManager : Widget
    {
        #region Singleton

        private static KeyboardManager singleton;

        public static KeyboardManager GetInstance()
        {
            return singleton ?? (singleton = new KeyboardManager());
        }

        #endregion Singleton

        #region Latch State

        private KeyboardState currentState;

        private KeyboardState previousState;

        public KeyboardState CurrentState
        {
            get
            {
                return currentState;
            }
        }

        public KeyboardState PreviousState
        {
            get
            {
                return previousState;
            }
        }

        #endregion Latch State

        #region Modifier State

        public bool AltDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftAlt) || state.IsKeyDown(Keys.RightAlt);
            }
        }

        public bool CtrlDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.RightControl);
            }
        }

        public bool ShiftDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift);
            }
        }

        #endregion Modifier State

        #region Action States

        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        public bool IsActionPressed(Actions action)
        {
            return IsActionMapPressed(actionMaps[(int)action]);
        }

        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        public bool IsActionTriggered(Actions action)
        {
            return IsActionMapTriggered(actionMaps[(int)action]);
        }

        /// <summary>
        /// Check if an action map has been pressed.
        /// </summary>
        private bool IsActionMapPressed(KeyboardActionMap actionMap)
        {
            return
                actionMap.Bindings.Any(
                    binding =>
                    IsKeyPressed(binding.Key) && (binding.Value.Count == 0 || binding.Value.All(IsKeyPressed)));
        }

        /// <summary>
        /// Check if an action map has been triggered this frame.
        /// </summary>
        private bool IsActionMapTriggered(KeyboardActionMap actionMap)
        {
            return
                actionMap.Bindings.Any(
                    binding =>
                    IsKeyTriggered(binding.Key) && (binding.Value.Count == 0 || binding.Value.Any(IsKeyPressed)));
        }

        #endregion Action States

        #region Key States

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        public bool IsKeyPressed(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public bool IsKeyTriggered(Keys key)
        {
            return currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
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
        /// The action system hierarchy: Action - KeyboardActionMap - Bindings
        /// </summary>
        private static KeyboardActionMap[] actionMaps;

        public static KeyboardActionMap[] ActionMaps
        {
            get
            {
                return actionMaps;
            }
        }

        /// <summary>
        /// Reset the action maps to their default values.
        /// </summary>
        private static void ResetActionMaps()
        {
            actionMaps = new KeyboardActionMap[(int)Actions.ActionNum];

            // cursor movement
            // -----------------------------------------------------------------
            actionMaps[(int)Actions.Up] = new KeyboardActionMap();
            actionMaps[(int)Actions.Up].Bindings.Add(Keys.K, new List<Keys>());

            actionMaps[(int)Actions.Down] = new KeyboardActionMap();
            actionMaps[(int)Actions.Down].Bindings.Add(Keys.J, new List<Keys>());

            actionMaps[(int)Actions.Left] = new KeyboardActionMap();
            actionMaps[(int)Actions.Left].Bindings.Add(Keys.H, new List<Keys>());

            actionMaps[(int)Actions.Right] = new KeyboardActionMap();
            actionMaps[(int)Actions.Right].Bindings.Add(Keys.L, new List<Keys>());

            actionMaps[(int)Actions.SUp] = new KeyboardActionMap();
            actionMaps[(int)Actions.SUp].Bindings.Add(Keys.K, new List<Keys> { Keys.LeftControl });

            actionMaps[(int)Actions.SDown] = new KeyboardActionMap();
            actionMaps[(int)Actions.SDown].Bindings.Add(Keys.J, new List<Keys> { Keys.LeftControl });

            actionMaps[(int)Actions.SLeft] = new KeyboardActionMap();
            actionMaps[(int)Actions.SLeft].Bindings.Add(Keys.H, new List<Keys> { Keys.LeftControl });

            actionMaps[(int)Actions.SRight] = new KeyboardActionMap();
            actionMaps[(int)Actions.SRight].Bindings.Add(Keys.L, new List<Keys> { Keys.LeftControl });

            // list management
            // -----------------------------------------------------------------
            // motivation
            actionMaps[(int)Actions.MotivationCreateItem] = new KeyboardActionMap();
            actionMaps[(int)Actions.MotivationCreateItem].Bindings.Add(Keys.O, new List<Keys> { Keys.LeftShift });

            actionMaps[(int)Actions.MotivationDeleteItem] = new KeyboardActionMap();
            actionMaps[(int)Actions.MotivationDeleteItem].Bindings.Add(Keys.D, new List<Keys> { Keys.LeftShift });

            actionMaps[(int)Actions.MotivationEditItem] = new KeyboardActionMap();
            actionMaps[(int)Actions.MotivationEditItem].Bindings.Add(Keys.I, new List<Keys> { Keys.LeftShift });

            // task
            //-----------------------------------------------------------------
            actionMaps[(int)Actions.TaskCreateItem] = new KeyboardActionMap();
            actionMaps[(int)Actions.TaskCreateItem].Bindings.Add(Keys.O, new List<Keys> { Keys.LeftControl });

            actionMaps[(int)Actions.TaskDeleteItem] = new KeyboardActionMap();
            actionMaps[(int)Actions.TaskDeleteItem].Bindings.Add(Keys.D, new List<Keys> { Keys.LeftControl });

            actionMaps[(int)Actions.TaskEditItem] = new KeyboardActionMap();
            actionMaps[(int)Actions.TaskEditItem].Bindings.Add(Keys.I, new List<Keys> { Keys.LeftControl });

            // sychronization
            // ---------------------------------------------------------------------
            actionMaps[(int)Actions.ForceReset] = new KeyboardActionMap();
            actionMaps[(int)Actions.ForceReset].Bindings.Add(Keys.R, new List<Keys> { Keys.Escape });

            // general
            // ---------------------------------------------------------------------
            actionMaps[(int)Actions.Escape] = new KeyboardActionMap();
            actionMaps[(int)Actions.Escape].Bindings.Add(Keys.C, new List<Keys> { Keys.LeftControl });
            actionMaps[(int)Actions.Escape].Bindings.Add(Keys.Escape, new List<Keys>());

            actionMaps[(int)Actions.Enter] = new KeyboardActionMap();
            actionMaps[(int)Actions.Enter].Bindings.Add(Keys.Space, new List<Keys>());
        }

        #endregion Keyboard Action Mappings

        #region Update

        public override void Draw(GameTime gameTime, byte alpha)
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public override void UpdateStructure(GameTime gameTime)
        {
        }

        #endregion Update
    }
}