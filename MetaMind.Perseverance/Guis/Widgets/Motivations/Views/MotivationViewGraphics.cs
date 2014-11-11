using System;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewGraphics : ViewBasicGraphics
    {
        public MotivationViewGraphics( IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            base.Draw( gameTime, alpha);
        }
    }
}