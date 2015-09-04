namespace MetaMind.Engine.Guis.Widgets.Visuals
{
    using Engine;
    using Components.Fonts;
    using Services;
    using Microsoft.Xna.Framework;

    public class LabelBox : GameVisualEntity
    {
        private readonly LabelSettings labelSettings;

        private readonly BoxSettings boxSettings;

        private readonly Vector2 labelMargin;

        private Point boxMargin = new Point(2, 2);

        public LabelBox(LabelSettings labelSettings, Vector2 labelMargin, BoxSettings boxSettings)
        {
            this.labelSettings = labelSettings;
            this.labelMargin   = labelMargin;
            this.boxSettings   = boxSettings;

            this.Label = new Label
            {
                TextFont       = () => this.labelSettings.TextFont,
                Text           = () => this.TextWrapped(),
                TextPosition   = () => this.labelSettings.TextPosition() + this.labelMargin,
                TextColor      = () => this.labelSettings.TextColor,
                TextSize       = () => this.labelSettings.TextSize,

                TextLeading    = this.labelSettings.TextLeading,
                TextMonospaced = this.labelSettings.TextMonospaced,
                TextHAlign     = this.labelSettings.TextHAlign,
                TextVAlign     = this.labelSettings.TextVAlign,
            };

            this.Box = new Box(
                () =>
                {
                    var rectangle = this.boxSettings.Bounds();
                    return new Rectangle(rectangle.Location, new Point(rectangle.Width, (this.TextWrapped().Split('\n').Length - 1) * this.labelSettings.TextLeading)).Crop(this.boxMargin);
                },
                () => this.boxSettings.Color(),
                () => this.boxSettings.ColorFilled());
        }

        private string TextWrapped()
        {
            return StringUtils.BreakStringByWord(
                this.labelSettings.TextFont,
                this.labelSettings.Text(),
                this.labelSettings.TextSize,
                this.boxSettings.Bounds().Width,
                this.labelSettings.TextMonospaced);
        }

        public Point BoxMargin
        {
            get { return this.boxMargin; }
            set { this.boxMargin = value; }
        }

        public Vector2 Location => this.Box.Bounds().Location.ToVector2();

        public int Bottom => this.Box.Bounds().Bottom;

        public Box Box { get; set; }

        public Label Label { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);

            this.Box  .Draw(graphics, time, alpha);
            this.Label.Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Box  .Update(time);
            this.Label.Update(time);
        }
    }
}