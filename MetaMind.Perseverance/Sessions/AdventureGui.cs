using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Perseverance.Guis.Widgets.Feelings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Sessions
{
    public class AdventureGui : Widget
    {
        private IWidget flow;

        public AdventureGui()
        {
            flow = new FeelingWidget();
        }

        public override void Draw(GameTime gameTime)
        {
            flow.Draw(gameTime);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            flow.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            flow.UpdateStructure(gameTime);
        }
    }
}
