namespace MetaMind.Testimony.Guis.Widgets.BlockViews.Options
{
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Settings;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;

    public class OptionItemSettings : ItemSettings
    {
        private readonly FrameSettings rootFrame = new FrameSettings
        {
            Size   = new Point(24 + 128 + 1355, 26),
            Margin = new Point(2, 2)
        };

        private readonly FrameSettings idFrame = new FrameSettings
        {
            Size   = new Point(24, 26),
            Margin = new Point(2, 2),

            RegularColor      = Palette.DarkRed,
            MouseOverColor    = Palette.DarkRed,
            PendingColor      = Palette.LightYellow,
            ModificationColor = Palette.DarkRed,
            SelectionColor    = Palette.DarkRed,
        };

        private readonly LabelSettings idLabel = new LabelSettings
        {
            TextSize   = 0.7f,
            TextColor  = Color.White,
            TextFont   = Font.UiStatistics,
            TextHAlign = StringHAlign.Center,
            TextVAlign = StringVAlign.Center,
        };

        private readonly FrameSettings nameFrame = new FrameSettings
        {
            Size   = new Point(1355, 26),
            Margin = new Point(2, 2),

            RegularColor      = Palette.DimBlue,
            MouseOverColor    = Palette.DimBlue,
            PendingColor      = Palette.LightBlue,
            ModificationColor = Palette.DimBlue,
            SelectionColor    = Palette.LightBlue,
        };

        private readonly LabelSettings nameLabel = new LabelSettings
        {
            TextSize       = 0.8f,
            TextColor      = Color.White,
            TextFont       = Font.ContentRegular,
            TextHAlign     = StringHAlign.Right,
            TextVAlign     = StringVAlign.Center,
            TextLeading    = 26,

            TextMonospaced = true,
        };

        private readonly FrameSettings descriptionFrame = new FrameSettings
        {
            Size   = new Point(1355, 26),
            Margin = new Point(2, 2),

            RegularColor      = Palette.Transparent0,
            MouseOverColor    = Palette.DimBlue,
            PendingColor      = Palette.Transparent3,
            ModificationColor = Palette.Transparent0,
            SelectionColor    = Palette.Transparent3,
        };

        private readonly LabelSettings descriptionLabel = new LabelSettings
        {
            TextSize       = 0.8f,
            TextColor      = Color.White,
            TextFont       = Font.ContentBold,
            TextHAlign     = StringHAlign.Right,
            TextVAlign     = StringVAlign.Center,
            TextLeading    = 26,

            TextMonospaced = true,
        };


        public OptionItemSettings()
        {
            this.Add("RootFrame", this.rootFrame);

            this.Add("IdFrame", this.idFrame);
            this.Add("IdLabel", this.idLabel);

            this.Add("NameFrame", this.nameFrame);
            this.Add("NameLabel", this.nameLabel);
            this.Add("NameMargin", new Vector2(5, 12) * this.nameLabel.TextSize);

            this.Add("DescriptionFrame", this.descriptionFrame);
            this.Add("DescriptionLabel", this.descriptionLabel);
            this.Add("DescriptionMargin", new Vector2(5, 12) * this.descriptionLabel.TextSize);
        }
    }
}
