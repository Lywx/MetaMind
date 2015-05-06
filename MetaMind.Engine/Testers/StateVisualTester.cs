// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StateVisualTester.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Testers
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public static class StateVisualTester
    {
        public static void Draw(IGameGraphicsService graphics, Type state, bool[] states, Vector2 start, int dx, int dy)
        {
            for (var i = 0; i < states.Count(); ++i)
            {
                if (states[i])
                {
                    var text = Enum.GetName(state, i);
                    var position = start + new Vector2(dx, dy * i);

                    graphics.StringDrawer.DrawString(Font.UiStatistics, text, position, Color.White, 1f);
                }
            }
        }
    }
}