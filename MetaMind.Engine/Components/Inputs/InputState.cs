// --------------------------------------------------------------------------------------------------------------------
// <copyright file="State.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using System;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// This is a game component that implements IOuterUpdateable.
    /// </summary>
    public class InputState : GameControllableComponent, IInputState
    {
        private readonly KeyboardInputState keyboard;

        private readonly MouseInputState mouse;

        public IKeyboardInputState Keyboard => this.keyboard;

        public IMouseInputState Mouse => this.mouse;

        #region Constructors

        public InputState(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.keyboard = new KeyboardInputState();
            this.mouse    = new MouseInputState();
        }

        #endregion Constructors

        #region Update and Draw

        public override void UpdateInput(GameTime gameTime)
        {
            this.mouse   .UpdateInput(gameTime);
            this.keyboard.UpdateInput(gameTime);
        }

        #endregion Update and Draw
    }
}