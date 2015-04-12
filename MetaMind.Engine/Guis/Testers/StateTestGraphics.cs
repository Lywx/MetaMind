namespace MetaMind.Engine.Guis.Testers
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Extensions;
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class StateTestGraphics : DrawableGameEntity
    {
        private Type enumType;

        private bool[] states;

        #region Constructors

        public StateTestGraphics(bool[] states, Type enumType)
        {
            this.states = states;
            this.enumType = enumType;
        }

        #endregion Constructors

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
        }

        public void DrawStates(IGameGraphics gameGraphics, Point start, int dx, int dy)
        {
            for (var i = 0; i < this.states.Count(); ++i)
            {
                if (this.states[i])
                {
                    var text     = Enum.GetName(this.enumType, i);
                    var position = ExtPoint.ToVector2(start) + new Vector2(dx, i * dy);

                    gameGraphics.Font.DrawString(Font.UiStatistics, text, position, Color.White, 1f);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Update(IGameFile gameFile, GameTime gameTime)
        {
        }

        public override void Update(IGameInterop gameInterop, GameTime gameTime)
        {
        }

        public override void Update(IGameSound gameSound, GameTime gameTime)
        {
        }
    }
}