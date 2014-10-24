namespace MetaMind.Engine.Guis.Widgets
{
    public abstract class InputObject : EngineObject, IInputObject
    {
        #region Input Handling Counter

        private const int HandlingInputCountOut = 1;

        private int handlingInputCount;

        #endregion Input Handling Counter

        #region Input Handling and Updating State

        public bool IsHandlingInput { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is updating.
        /// Currently there is no implementation to set it to false.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is updating; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpdating { get; private set; }

        #endregion Input Handling and Updating State

        #region Contructors

        protected InputObject()
        {
            IsUpdating = true;
            IsHandlingInput = false;
        }

        #endregion Contructors

        #region Update and Draw

        public abstract void Draw( Microsoft.Xna.Framework.GameTime gameTime );

        public virtual void HandleInput()
        {
            IsHandlingInput = true;
            handlingInputCount = 0;
        }

        public abstract void UpdateInput( Microsoft.Xna.Framework.GameTime gameTime );

        public abstract void UpdateStructure( Microsoft.Xna.Framework.GameTime gameTime );

        public void Update( Microsoft.Xna.Framework.GameTime gameTime )
        {
            if ( !IsUpdating )
                return;

            ++handlingInputCount;
            // use this mechanism to make sure when HandleInput is used
            // Update method won't set the IsActive to false, which happens is cases
            // of not using HandleInput in GameScreen class.
            if ( handlingInputCount > HandlingInputCountOut )
            {
                IsHandlingInput = false;
                handlingInputCount = 0;
            }

            if ( IsHandlingInput )
                UpdateInput( gameTime );
            UpdateStructure( gameTime );
        }

        #endregion Update and Draw
    }
}