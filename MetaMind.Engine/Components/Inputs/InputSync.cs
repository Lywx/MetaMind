namespace MetaMind.Engine.Components.Inputs
{
    using Microsoft.Xna.Framework;

    public abstract class InputSync : GameComponent
    {
        #region Input Handling Counter

        private const int InputSyncCountOut = 1;

        private int inputCount;

        #endregion Input Handling Counter

        #region State Data

        /// <summary>
        /// Gets a value indicating whether this instance is updating input.
        /// </summary>
        public bool Controllable { get; private set; }

        #endregion 

        #region Contructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <param name="updateOrder">It is very sensible. It need to be higher 
        /// than the sync standard's value so that once this instance finished 
        /// updating input(which a process between calling UpdateInput method 
        /// and calling Update method), it is freezed before sync standard 
        /// updating. </param>
        protected InputSync(GameEngine gameEngine, int updateOrder)
            : base(gameEngine)
        {
            this.UpdateOrder = updateOrder;
        }

        #endregion Contructors

        #region Update and Draw

        /// <summary>
        /// Hook up with sync standard to keep in sync
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.inputCount += 1;

            // use this mechanism to make sure when UpdateInput is used
            // Update method won't set the Enabled to false, which happens is cases
            // of not using UpdateInput in GameScreen class.
            if (this.inputCount > InputSyncCountOut)
            {
                this.Reset();

                this.Controllable = false;
            }
        }

        /// <summary>
        /// Enable event firing that is in sync with standard. It must be called before 
        /// the sync standarad's Update method.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateInput(GameTime gameTime)
        {
            this.Reset();

            this.Controllable = true;
        }

        private void Reset()
        {
            this.inputCount = 0;
        }
        #endregion Update and Draw
    }
}
