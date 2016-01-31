namespace MetaMind.Engine.Core.Backend.Input
{
    using System;
    using Keyboard;
    using Microsoft.Xna.Framework;
    using Mouse;

    /// <summary>
    ///     This is a game component that implements IMMUpdateable.
    /// </summary>
    public class MMInputState : MMGeneralComponent, IMMInputState
    {
        #region Constructors

        public MMInputState(MMEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Keyboard = new MMKeyboardInput();
            this.Mouse = new MMMouseInput();
        }

        #endregion Constructors

        public IMMMouseInput Mouse { get; }

        public IMMKeyboardInput Keyboard { get; } 

        #region Update and Draw

        public override void UpdateInput(GameTime gameTime)
        {
            this.Mouse.UpdateInput(gameTime);
            this.Keyboard.UpdateInput(gameTime);
        }

        #endregion Update and Draw
    }
}
