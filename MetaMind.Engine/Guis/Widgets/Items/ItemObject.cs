using System;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Items
{
    public interface IItemObject
    {
        ICloneable ItemSettings { get; }

        void Draw( GameTime gameTime, byte alpha );

        void Update( GameTime gameTime );

        void Disable( ItemState state );

        void Enable( ItemState state );

        bool IsEnabled( ItemState state );
    }

    public abstract class ItemObject : EngineObject, IItemObject
    {
        private ICloneable itemSettings;

        private bool[] states;

        protected ItemObject( ICloneable itemSettings )
        {
            this.itemSettings = itemSettings;
            states = new bool[ ( int ) ItemState.StateNum ];
        }

        public ICloneable ItemSettings
        {
            get { return itemSettings; }
            protected set { itemSettings = value; }
        }
        public bool[ ] States { get { return states; } }

        #region State Operation

        public void Disable( ItemState state )
        {
            state.DisableStateIn( states );
        }

        public void Enable( ItemState state )
        {
            state.EnableStateIn( states );
        }

        public bool IsEnabled( ItemState state )
        {
            return state.IsStateEnabledIn( states );
        }

        #endregion State Operation

        #region Update and Draw

        public abstract void Draw(GameTime gameTime, byte alpha);

        public abstract void Update( GameTime gameTime );

        #endregion Update and Draw
    }
}