// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationViewGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;

    using Microsoft.Xna.Framework;

    public class MotivationViewGraphics : ViewBasicGraphics
    {
        public MotivationViewGraphics(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            base.Draw(gameTime, alpha);
            
            // TODO: REMOVE
            // draw state test 
            var test = new StateTestGraphics(View.States, typeof(ViewState));
            test.DrawStates(ViewSettings.StartPoint, 300, 25);
        }
    }
}