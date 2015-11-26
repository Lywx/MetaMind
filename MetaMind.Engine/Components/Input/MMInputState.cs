namespace MetaMind.Engine.Components.Input
{
    using System;
    using Microsoft.Xna.Framework;
    using Mouse;

    /// <summary>
    ///     This is a game component that implements IMMUpdateable.
    /// </summary>
    public class MMInputState : MMInputableComponent, IMMInputState
    {
        private readonly MMKeyboardInput keyboard;

        private readonly MMMouseInput mouse;

        #region Constructors

        public MMInputState(MMEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.keyboard = new MMKeyboardInput();
            this.mouse = new MMMouseInput();
        }

        #endregion Constructors

        public IMMMouseInput Mouse => this.mouse;

        public IMMKeyboardInput Keyboard => this.keyboard;

        #region Update and Draw

        public override void UpdateInput(GameTime gameTime)
        {
            this.mouse.UpdateInput(gameTime);
            this.keyboard.UpdateInput(gameTime);
        }

        #endregion Update and Draw
    }
}
