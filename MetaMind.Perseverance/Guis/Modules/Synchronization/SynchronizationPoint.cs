namespace MetaMind.Perseverance.Guis.Modules.Synchronization
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class SynchronizationPoint : GameVisualEntity
    {
        public SynchronizationPoint(string text, Func<Color> textColor, Func<Color> boxColor, Func<Rectangle> boxbounds)
        {
            this.Digit = new Label(
                () => Font.UiStatistics,
                () => text,
                () => boxbounds().Center.ToVector2() + new Vector2(0, 16),
                textColor,
                () => 0.7f) {
                                TextHAlign = StringHAlign.Center, TextVAlign = StringVAlign.Center 
                            };

            this.Box = new Box(boxbounds, boxColor, () => true);
        }

        public Box Box { get; set; }

        public Label Digit { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Box  .Draw(graphics, time, alpha);
            this.Digit.Draw(graphics, time, alpha);
        }
    }
}