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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputState : GameComponent, IInputState
    {
        private readonly KeyboardInputState keyboard;

        private readonly MouseInputState mouse;

        public IKeyboardInputState Keyboard
        {
            get
            {
                return this.keyboard;
            }
        }

        public IMouseInputState Mouse
        {
            get
            {
                return this.mouse;
            }
        }

        #region Constructors

        public InputState(GameEngine engine, int updateOrder)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            this.Game.Components.Add(this);
            
            this.UpdateOrder = updateOrder;

            this.keyboard = new KeyboardInputState();
            this.mouse    = new MouseInputState();
        }

        #endregion Constructors

        #region Update and Draw

        public void UpdateInput(GameTime gameTime)
        {
            this.mouse   .UpdateInput(gameTime);
            this.keyboard.UpdateInput(gameTime);
        }

        #endregion Update and Draw
    }
}