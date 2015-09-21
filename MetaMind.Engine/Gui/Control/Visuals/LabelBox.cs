namespace MetaMind.Engine.Gui.Control.Visuals
{
    using Extensions;
    using Microsoft.Xna.Framework;
    using Service;

    public class LabelBox : Control
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
                TextFont       = () => this.labelSettings.Font,
                Text           = () => this.TextWrapped(),
                AnchorLocation = () => this.labelSettings.AnchorLocation() + this.labelMargin,
                TextColor      = () => this.labelSettings.Color,
                TextSize       = () => this.labelSettings.Size,

                TextLeading    = this.labelSettings.Leading,
                TextMonospaced = this.labelSettings.Monospaced,
                TextHAlignment     = this.labelSettings.HAlignment,
                TextVAlignment     = this.labelSettings.VAlignment,
            };

            this.Box = new Box(
                () =>
                {
                    var rectangle = this.boxSettings.Bounds();
                    return new Rectangle(rectangle.Location, new Point(rectangle.Width, (this.TextWrapped().Split('\n').Length - 1) * this.labelSettings.Leading)).Crop(this.boxMargin);
                },
                () => this.boxSettings.Color(),
                () => this.boxSettings.ColorFilled());
        }

        private string TextWrapped()
        {
            return StringUtils.BreakStringByWord(
                this.labelSettings.Font,
                this.labelSettings.Text(),
                this.labelSettings.Size,
                this.boxSettings.Bounds().Width,
                this.labelSettings.Monospaced);
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