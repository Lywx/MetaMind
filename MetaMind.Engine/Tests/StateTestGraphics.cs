namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using System;
    using System.Linq;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    public class StateTestGraphics : EngineObject
    {
        private bool[] states;

        private Type type;

        public StateTestGraphics(bool[] states, Type type)
        {
            this.states = states;
            this.type = type;
        }

        public void DrawStates(Point start, int dx, int dy)
        {
            for (var i = 0; i < this.states.Count(); ++i)
            {
                if (this.states[i])
                {
                    var text = Enum.GetName(this.type, i);
                    var position = PointExt.ToVector2(start) + new Vector2(dx, i * dy);
                    FontManager.DrawText(Font.UiStatisticsFont, text, position, Color.White, 1f);
                }
            }
        }
    }
}