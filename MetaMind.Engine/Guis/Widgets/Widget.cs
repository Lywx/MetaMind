using System;
using System.Runtime.Serialization;

namespace MetaMind.Engine.Guis.Widgets
{
    public abstract class Widget : EngineObject, IWidget
    {
        #region Control Data

        public static readonly int HandlingInputCountOut = 1;

        private int handlingInputCount = 0;

        public bool IsHandlingInput { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is updating.
        /// Currently there is no implementation to set it to false.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is updating; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpdating { get; private set; }

        #endregion Control Data

        #region Contructors

        public Widget()
        {
            IsUpdating = true;
            IsHandlingInput = false;
        }

        #endregion Contructors

        #region Update and Draw

        public abstract void Draw(Microsoft.Xna.Framework.GameTime gameTime);

        public virtual void HandleInput()
        {
            IsHandlingInput = true;
            handlingInputCount = 0;
        }

        public abstract void UpdateInput(Microsoft.Xna.Framework.GameTime gameTime);

        public abstract void UpdateStructure(Microsoft.Xna.Framework.GameTime gameTime);

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
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