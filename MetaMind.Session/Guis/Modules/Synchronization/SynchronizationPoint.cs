namespace MetaMind.Session.Guis.Modules.Synchronization
{
    using System;
    using Engine.Components.Content.Fonts;
    using Engine.Entities;
    using Engine.Gui.Controls.Images;
    using Engine.Gui.Controls.Labels;
    using Engine.Gui.Graphics.Fonts;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class SynchronizationPoint : MMVisualEntity
    {
        public SynchronizationPoint(string text, Func<Color> textColor, Func<Color> boxColor, Func<Rectangle> boxbounds)
        {
            this.Digit = new Label(
                () => Font.UiStatistics,
                () => text,
                () => boxbounds().Center.ToVector2() + new Vector2(0, 16),
                textColor,
                () => 0.7f) {
                                TextHAlignment = HoritonalAlignment.Center, TextVAlignment = VerticalAlignment.Center 
                            };

            this.Box = new ImageBox(boxbounds, boxColor, () => true);
        }

        public ImageBox Box { get; set; }

        public Label Digit { get; set; }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time)
        {
            this.Box  .Draw(graphics, time);
            this.Digit.Draw(graphics, time);
        }
    }
}