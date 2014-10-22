using System;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingViews
{
    public class FeelingViewGraphics : ViewBasicGraphics
    {
        public FeelingViewGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );
        }
    }
}