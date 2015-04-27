namespace MetaMind.Engine.Components.Inputs
{
    using Microsoft.Xna.Framework;

    public abstract class InputSync : GameComponent
    {
        #region Input Handling Counter

        /// <summary>
        /// This is used to determine how many events is triggered as an input. 
        /// It is based on the idea that msg is sequential on different input type.
        /// </summary>
        private const int InputCountSyncThreshod = 6;

        private int InputCount { get; set; }

        #endregion Input Handling Counter

        #region State Data

        protected bool AcceptInput { get; set; }

        #endregion 

        #region Contructors

        protected InputSync(GameEngine engine, int updateOrder)
            : base(engine)
        {
            this.UpdateOrder = updateOrder;
        }

        #endregion Contructors

        #region Update and Draw

        public void UpdateInput(GameTime gameTime)
        {
            this.ResetInput();
        }

        protected void SyncInput()
        {
            this.InputCount += 1;

            if (this.InputCount >= InputCountSyncThreshod)
            {
                this.AcceptInput = false;
            }
        }

        private void ResetInput()
        {
            this.InputCount = 0;

            this.AcceptInput = true;
        }
        #endregion Update and Draw
    }
}
