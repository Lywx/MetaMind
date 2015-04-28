namespace MetaMind.Engine.Testers
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public static class StateVisualTester
    {
        public static void Draw(IGameGraphicsService graphics, bool[] states, Type state, Point start, int dx, int dy)
        {
            for (var i = 0; i < states.Count(); ++i)
            {
                if (states[i])
                {
                    var text     = Enum.GetName(state, i);
                    var position = ExtPoint.ToVector2(start) + new Vector2(dx, i * dy);

                    graphics.StringDrawer.DrawString(Font.UiStatistics, text, position, Color.White, 1f);
                }
            }
        }
    }
}