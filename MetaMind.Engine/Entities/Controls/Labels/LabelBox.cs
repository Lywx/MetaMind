namespace MetaMind.Engine.Entities.Controls.Labels
{
    using Extensions;
    using Images;
    using Microsoft.Xna.Framework;

    // TODO: Duplicate to button
    public class LabelBox : MMControlComponent
    {
        private readonly LabelSettings labelSettings;

        private readonly Vector2 labelMargin;

        private Point boxMargin = new Point(2, 2);

        public LabelBox(LabelSettings labelSettings, Vector2 labelMargin)
        {
            this.labelSettings = labelSettings;
            this.labelMargin   = labelMargin;

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

            this.Box = new ImageBox(
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

        public ImageBox Box { get; set; }

        public Label Label { get; set; }

        public override void Draw(GameTime time)
        {
            base.Draw(time);

            this.Box  .Draw(time);
            this.Label.Draw(time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Box  .Update(time);
            this.Label.Update(time);
        }
    }
}