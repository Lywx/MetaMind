using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewObject : IWidget
    {
        ICloneable ViewSettings { get; }
        ICloneable ItemSettings { get; }

        void Disable( ViewState state );

        void Enable( ViewState state );

        bool IsEnabled( ViewState state );
    }

    public class ViewObject : InputObject, IViewObject
    {
        private ICloneable viewSettings;
        private ICloneable itemSettings;
        public ICloneable ViewSettings { get { return viewSettings; } }
        public ICloneable ItemSettings { get { return itemSettings; } }

        protected ViewObject( ICloneable viewSettings, ICloneable itemSettings )
        {
            this.viewSettings = viewSettings;
            this.itemSettings = itemSettings;

            Enable( ViewState.View_Active );
        }

        private bool[] states = new bool[ ( int ) ViewState.StateNum ];
        public bool[ ] States { get { return states; } }

        public override void Draw( GameTime gameTime )
        {
        }

        public override void UpdateInput( GameTime gameTime )
        {
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        #region Helper Methods

        public void Disable( ViewState state )
        {
            state.DisableStateIn( states );
        }

        public void Enable( ViewState state )
        {
            state.EnableStateIn( states );
        }

        public bool IsEnabled( ViewState state )
        {
            return state.IsStateEnabledIn( states );
        }

        #endregion Helper Methods
    }
}