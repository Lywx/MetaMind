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

            this.Mouse    = new MMMouseInput();
            this.Keyboard = new MMKeyboardInput();
        }

        #endregion Constructors

        public IMMMouseInput Mouse { get; }

        public IMMKeyboardInput Keyboard { get; } 

        #region Update and Draw

        public override void UpdateInput(GameTime time)
        {
            this.Mouse   .UpdateInput(time);
            this.Keyboard.UpdateInput(time);
        }

        #endregion Update and Draw
    }
}
