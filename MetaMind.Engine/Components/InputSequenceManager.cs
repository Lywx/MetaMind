// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sequence.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputSequenceManager : ManualInputGameElement
    {
        private readonly KeyboardManager keyboard;

        private readonly MouseManager mouse;

        public KeyboardManager Keyboard
        {
            get
            {
                return this.keyboard;
            }
        }

        public MouseManager Mouse
        {
            get
            {
                return this.mouse;
            }
        }

        #region Singleton

        private static InputSequenceManager Singleton { get; set; }

        public static InputSequenceManager GetInstance()
        {
            return Singleton ?? (Singleton = new InputSequenceManager());
        }

        #endregion Singleton

        #region Constructors

        private InputSequenceManager()
        {
            this.keyboard = KeyboardManager.GetInstance();
            this.mouse = MouseManager.GetInstance();
        }

        #endregion Constructors

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.mouse   .UpdateInput(gameInput, gameTime);
            this.keyboard.UpdateInput(gameInput, gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
        }

        #endregion Update and Draw
    }
}