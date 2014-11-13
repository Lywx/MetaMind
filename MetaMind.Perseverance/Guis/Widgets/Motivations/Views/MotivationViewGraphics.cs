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
    }
}