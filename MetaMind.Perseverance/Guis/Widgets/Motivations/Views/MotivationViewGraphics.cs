using System;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewGraphics : ViewBasicGraphics
    {
        public MotivationViewGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            base.Draw( gameTime, alpha);
        }
    }
}