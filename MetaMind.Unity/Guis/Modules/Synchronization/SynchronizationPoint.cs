namespace MetaMind.Unity.Guis.Modules.Synchronization
{
    using System;
    using Engine;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics.Fonts;
    using Engine.Gui.Controls.Images;
    using Engine.Gui.Controls.Labels;
    using Engine.Service;
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

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Box  .Draw(graphics, time, alpha);
            this.Digit.Draw(graphics, time, alpha);
        }
    }
}