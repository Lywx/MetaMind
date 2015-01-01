// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MetaMind.Engine.Guis;
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

        TraceCreateItem, 
        TraceDeleteItem, 
        TraceEditItem, 
        TraceClearItem, 

        KnowledgeEditItem,

        CommandClearItem,
        CommandDeleteItem,
        CommandResetItem,
        CommandSortItem,

        // synchronization
        // ---------------------------------------------------------------------
        ForceFlip,
        ForceReset,
        ForceReverse,

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
                    IsKeyTriggered(binding.Key) && (binding.Value.Count == 0 || binding.Value.All(IsKeyPressed)));
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
            this.ResetActionMap();
            this.LoadActionMapFromFile();
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
            get { return actionMaps; }
        }

        private void ResetActionMap()
        {
            actionMaps = new KeyboardActionMap[(int)Actions.ActionNum];

            for (var i = 0; i < (int)Actions.ActionNum; i++)
            {
                actionMaps[i] = new KeyboardActionMap();
            }
        }

        private void LoadActionMapFromFile()
        {
            var lines = File.ReadAllLines(@"Configurations/Keyboard.txt");
            foreach (var line in lines)
            {
                this.LoadActionMapFromLine(line);
            }
        }

        private void LoadActionMapFromLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            var firstSplit  = line.Split('=');
            var secondSplit = firstSplit[1].Split(':');

            var actionName = firstSplit[0];
            Actions action;
            Actions.TryParse(actionName, true, out action);

            var keyName = secondSplit[0];
            Keys key;
            Keys.TryParse(keyName, true, out key);

            List<Keys> modifiers = new List<Keys>();

            if (secondSplit.Count() > 1)
            {
                foreach (var modifierName in secondSplit[1].Split(','))
                {
                    if (string.IsNullOrWhiteSpace(modifierName))
                    {
                        continue;
                    }

                    Keys modifier;
                    Keys.TryParse(modifierName, true, out modifier);

                    modifiers.Add(modifier); 
                }
            }

            actionMaps[(int)action].Bindings.Add(key, modifiers);
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