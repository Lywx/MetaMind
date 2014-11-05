using System;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewControl : ViewControl1D
    {
        public MotivationViewControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public override void UpdateInput( GameTime gameTime )
        {
            base.UpdateInput( gameTime );
        }
    }
}