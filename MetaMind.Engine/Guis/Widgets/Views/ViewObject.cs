using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewObject : IWidget
    {
        dynamic ViewSettings { get; }
        dynamic ItemSettings { get; }

        void Disable( ViewState state );

        void Enable( ViewState state );

        bool IsEnabled( ViewState state );
    }

    public class ViewObject : Widget, IViewObject
    {
        public dynamic ViewSettings { get; private set; }
        public dynamic ItemSettings { get; private set; }

        protected ViewObject( ICloneable viewSettings, ICloneable itemSettings )
        {
            ViewSettings = viewSettings;
            ItemSettings = itemSettings;

            Enable( ViewState.View_Active );
        }

        private bool[] states = new bool[ ( int ) ViewState.StateNum ];
        public bool[ ] States { get { return states; } }

        public override void Draw(GameTime gameTime, byte alpha)
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