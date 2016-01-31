namespace MetaMind.Engine.Core.Backend.Input.Keyboard
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Services.IO;

    public class MMKeyboardInput : IMMKeyboardInput, IMMPlainConfigurationFileLoader
    {
        private MMKeyboardBinding<MMInputAction> keyboardBinding;

        private KeyboardState keyboardCurrent;

        private KeyboardState keyboardPrevious;

        #region Constructors

        internal MMKeyboardInput()
        {
        }

        #endregion Constructors

        #region Update

        public void UpdateInput(GameTime time)
        {
            this.keyboardPrevious = this.keyboardCurrent;
            this.keyboardCurrent = Keyboard.GetState();
        }

        #endregion Update

        #region Modifier States

        public bool IsAltDown
            =>
                this.keyboardCurrent.IsKeyDown(Keys.LeftAlt)
                || this.keyboardCurrent.IsKeyDown(Keys.RightAlt);

        public bool IsCtrlDown
            =>
                this.keyboardCurrent.IsKeyDown(Keys.LeftControl)
                || this.keyboardCurrent.IsKeyDown(Keys.RightControl);

        public bool IsShiftDown
            =>
                this.keyboardCurrent.IsKeyDown(Keys.LeftShift)
                || this.keyboardCurrent.IsKeyDown(Keys.RightShift);

        private bool AreModifiersReady(KeyValuePair<Keys, List<Keys>> binding)
        {
            return this.AreModifiersEmpty(binding) || this.AreModifiersPressed(binding);
        }

        private bool AreModifiersPressed(KeyValuePair<Keys, List<Keys>> binding)
        {
            return binding.Value.All(this.IsKeyPressed);
        }

        private bool AreModifiersEmpty(KeyValuePair<Keys, List<Keys>> binding)
        {
            return binding.Value.Count == 0;
        }

        #endregion 

        #region Action States

        /// <summary>
        ///     Check if an action has been pressed.
        /// </summary>
        public bool IsActionPressed(MMInputAction action)
        {
            return this.IsKeyboardBindingPressed(this.keyboardBinding[action]);
        }

        /// <summary>
        ///     Check if an action was just performed in the most recent update.
        /// </summary>
        public bool IsActionTriggered(MMInputAction action)
        {
            return this.IsKeyboardBindingTriggered(this.keyboardBinding[action]);
        }

        #endregion

        #region Keyboard Binding States

        /// <summary>
        ///     Check if an action map has been pressed.
        /// </summary>
        private bool IsKeyboardBindingPressed(MMKeyboardCombination combination)
        {
            return
                combination.Any(binding => this.IsKeyPressed(binding.Key)
                                           && this.AreModifiersReady(binding));
        }

        /// <summary>
        ///     Check if an action map has been triggered this frame.
        /// </summary>
        private bool IsKeyboardBindingTriggered(MMKeyboardCombination combination)
        {
            return
                combination.Any(
                    binding => this.IsKeyTriggered(binding.Key)
                               && this.AreModifiersReady(binding));
        }

        #endregion

        #region Keyboard States

        /// <summary>
        ///     Check if a key is pressed.
        /// </summary>
        public bool IsKeyPressed(Keys key)
        {
            return this.keyboardCurrent.IsKeyDown(key);
        }

        /// <summary>
        ///     Check if a key was just pressed in the most recent update.
        /// </summary>
        public bool IsKeyTriggered(Keys key)
        {
            return this.keyboardCurrent.IsKeyDown(key)
                   && !this.keyboardPrevious.IsKeyDown(key);
        }

        #endregion 

        #region Operations

        public void LoadActions<TActions>() where TActions : MMInputActions
        {
            this.keyboardBinding = MMKeyboardBindingUtils.Load<TActions>(this);
        }

        #endregion

        #region Configurations

        public string ConfigurationFilename => "Control.ini";

        #endregion
    }
}