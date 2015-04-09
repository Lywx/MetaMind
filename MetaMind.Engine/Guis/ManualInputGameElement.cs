namespace MetaMind.Engine.Guis
{
    using System;

    using Microsoft.Xna.Framework;

    public abstract class ManualInputGameElement : DrawableGameElement, IManualInputable
    {
        #region Input Handling Counter

        private const int HandlingInputCountOut = 1;

        private int handlingInputCount;

        #endregion Input Handling Counter

        #region Input Handling and Updating State

        /// <summary>
        /// Whether this instance is updating input.
        /// </summary>
        public bool Controllable { get; private set; }

        #endregion Input Handling and Updating State

        #region Contructors

        protected ManualInputGameElement()
        {
            this.Controllable = false;
        }

        #endregion Contructors

        #region Update and Draw

        public abstract void Draw(GameTime gameTime, byte alpha);

        public virtual void HandleInput()
        {
            this.Controllable = true;
            this.handlingInputCount = 0;
        }

        public abstract void UpdateInput(IGameInput gameInput, GameTime gameTime);

        public abstract void UpdateStructure(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.handlingInputCount += 1;

            // use this mechanism to make sure when HandleInput is used
            // Update method won't set the IsActive to false, which happens is cases
            // of not using HandleInput in GameScreen class.
            if (this.handlingInputCount > HandlingInputCountOut)
            {
                this.Controllable = false;
                this.handlingInputCount = 0;
            }

            if (this.Controllable)
            {
                this.UpdateInput(gameInput, gameTime);
            }

            this.UpdateStructure(gameTime);
        }

        #endregion Update and Draw
    }
}