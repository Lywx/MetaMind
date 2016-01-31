namespace MetaMind.Session.Guis.Modules.Synchronization
{
    using System;
    using Engine.Core.Backend.Content.Fonts;
    using Engine.Core.Entity.Common;
    using Engine.Core.Entity.Control.Labels;
    using Engine.Core.Entity.Graphics.Fonts;
    using Microsoft.Xna.Framework;

    public class SynchronizationPoint : MMVisualEntity
    {
        public SynchronizationPoint(string text, Func<Color> textColor, Func<Color> boxColor, Func<Rectangle> boxbounds)
        {
            this.Digit = new Label(
                () => MMFont.UiStatistics,
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

        public override void Draw(GameTime time)
        {
            this.Box  .Draw(time);
            this.Digit.Draw(time);
        }
    }
}