namespace MetaMind.Engine.Testers
{
    using System;
    using System.Linq;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class StateGraphicTester : GameVisualEntity
    {
        private Type enumType;

        private bool[] states;

        #region Constructors

        public StateGraphicTester(bool[] states, Type enumType)
        {
            this.states = states;
            this.enumType = enumType;
        }

        #endregion Constructors

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }

        public void DrawStates(IGameGraphicsService graphics, Point start, int dx, int dy)
        {
            for (var i = 0; i < this.states.Count(); ++i)
            {
                if (this.states[i])
                {
                    var text     = Enum.GetName(this.enumType, i);
                    var position = ExtPoint.ToVector2(start) + new Vector2(dx, i * dy);

                    graphics.StringDrawer.DrawString(Font.UiStatistics, text, position, Color.White, 1f);
                }
            }
        }
    }
}