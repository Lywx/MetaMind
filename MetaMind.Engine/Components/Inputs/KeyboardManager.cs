// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardManager.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MetaMind.Engine.Parsers.Elements;
    using MetaMind.Engine.Parsers.Grammars;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using Sprache;

    /// <summary>
    /// The actions that are possible within the game.
    /// </summary>
    public enum Actions
    {
        // Cursor Movement
        Up, 
        Down, 
        Left, 
        Right, 

        FastUp, 
        FastDown, 
        FastLeft, 
        FastRight, 

        // List Management
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
        KnowledgeLoadBuffer,

        ModuleClearItem,
        ModuleDeleteItem,
        ModuleOpenItem,
        ModuleResetItem,
        ModuleSortItem,

        CommandClearItem,
        CommandDeleteItem,
        CommandOpenItem,
        CommandSortItem,

        // Synchronization
        ForceAwake,
        ForceFlip,
        ForceReset,
        ForceReverse,

        // General
        Enter, 
        Escape, 

        ActionNum,
    }

    public class KeyboardActionMap
    {
        /// <summary>
        /// Dictionary of key, modifier pair to be mapped to a given action.
        /// </summary>
        public Dictionary<Keys, List<Keys>> Bindings = new Dictionary<Keys, List<Keys>>();
    }

    public class KeyboardManager : GameControllableEntity, IConfigurationFileLoader
    {
        #region Singleton

        private static KeyboardManager Singleton { get; set; }

        public static KeyboardManager GetInstance()
        {
            return Singleton ?? (Singleton = new KeyboardManager());
        }

        #endregion Singleton

        #region Latch State

        private KeyboardState currentState;

        private KeyboardState previousState;

        public KeyboardState CurrentState
        {
            get
            {
                return this.currentState;
            }
        }

        public KeyboardState PreviousState
        {
            get
            {
                return this.previousState;
            }
        }

        #endregion Latch State

        #region Modifier State

        public bool AltDown
        {
            get
            {
                var state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftAlt) || state.IsKeyDown(Keys.RightAlt);
            }
        }

        public bool CtrlDown
        {
            get
            {
                var state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.RightControl);
            }
        }

        public bool ShiftDown
        {
            get
            {
                var state = Keyboard.GetState();
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
            return this.IsActionMapPressed(actionMaps[(int)action]);
        }

        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        public bool IsActionTriggered(Actions action)
        {
            return this.IsActionMapTriggered(actionMaps[(int)action]);
        }

        /// <summary>
        /// Check if an action map has been pressed.
        /// </summary>
        private bool IsActionMapPressed(KeyboardActionMap actionMap)
        {
            return
                actionMap.Bindings.Any(
                    binding =>
                    this.IsKeyPressed(binding.Key) && (binding.Value.Count == 0 || binding.Value.All(this.IsKeyPressed)));
        }

        /// <summary>
        /// Check if an action map has been triggered this frame.
        /// </summary>
        private bool IsActionMapTriggered(KeyboardActionMap actionMap)
        {
            return
                actionMap.Bindings.Any(
                    binding =>
                    this.IsKeyTriggered(binding.Key) && (binding.Value.Count == 0 || binding.Value.All(this.IsKeyPressed)));
        }

        #endregion Action States

        #region Action Mappings

        /// <summary>
        /// The action mappings for the game.
        /// The action system hierarchy: Action - KeyboardActionMap - Bindings
        /// </summary>
        private static KeyboardActionMap[] actionMaps;

        public static KeyboardActionMap[] ActionMaps
        {
            get { return actionMaps; }
        }

        private void ActionMapInitialize()
        {
            actionMaps = new KeyboardActionMap[(int)Actions.ActionNum];

            for (var i = 0; i < (int)Actions.ActionNum; i++)
            {
                actionMaps[i] = new KeyboardActionMap();
            }
        }

        private void ActionMapLoad()
        {
            foreach (var pair in ConfigurationFileLoader.LoadDuplicablePairs(this))
            {
                this.ActionMapPairLoad(pair);
            }
        }

        private void ActionMapPairLoad(KeyValuePair<string, string> pair)
        {
            // parse pair action
            Actions action;
            var success = Enum.TryParse(pair.Key, true, out action);
            if (!success)
            {
                return;
            }

            // parse pair mapping
            Keys key;
            List<Keys> modifiers = new List<Keys>();

            var expression = LineGrammar.SentenceParser.Parse(pair.Value);

            // parse mapping key
            key = Key(expression);

            // parse mapping modifier
            // case insensitive 
            if (expression.Words.Last() != "alone")
            {
                foreach (var modifierName in expression.Words.Skip(2))
                {
                    if (modifierName == "and")
                    {
                        continue;
                    }

                    Keys modifier;
                    Enum.TryParse(modifierName, true, out modifier);

                    modifiers.Add(modifier);
                }
            }

            actionMaps[(int)action].Bindings.Add(key, modifiers);
        }

        private static Keys Key(Sentence expression)
        {
            Keys key;
            Enum.TryParse(expression.Words[0], true, out key);
            return key;
        }

        #endregion Action Mappings

        #region Key States

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        public bool IsKeyPressed(Keys key)
        {
            return this.currentState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public bool IsKeyTriggered(Keys key)
        {
            return this.currentState.IsKeyDown(key) && !this.previousState.IsKeyDown(key);
        }

        #endregion Key States

        #region Constructors

        private KeyboardManager()
        {
            this.LoadConfiguration();
        }

        #endregion Constructors

        #region Configurations

        public string ConfigurationFile
        {
            get
            {
                return "Control.txt";
            }
        }

        public void LoadConfiguration()
        {
            this.ActionMapInitialize();
            this.ActionMapLoad();
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.previousState = this.currentState;
            this.currentState = Keyboard.GetState();
        }

        #endregion Update
    }
}