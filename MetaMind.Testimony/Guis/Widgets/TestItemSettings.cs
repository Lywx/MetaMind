namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Settings;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class TestItemSettings : ItemSettings
    {
        private readonly FrameSettings rootFrame = new FrameSettings
        {
            Size   = new Point(24 + 64 + 512, 26),
            Margin = new Point(2, 2)
        };

        private readonly FrameSettings idFrame = new FrameSettings
        {
            Size   = new Point(24, 26),
            Margin = new Point(2, 2),

            RegularColor      = Palette.DarkRed,
            MouseOverColor    = Palette.DarkRed,
            PendingColor      = Palette.DarkRed,
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
            Size   = new Point(512, 26),
            Margin = new Point(2, 2),

            RegularColor      = Palette.Transparent0,
            MouseOverColor    = Palette.DimBlue,
            PendingColor      = Palette.Transparent0,
            ModificationColor = Palette.Transparent0,
            SelectionColor    = Palette.LightBlue,
        };

        private readonly LabelSettings nameLabel = new LabelSettings
        {
            TextSize       = 0.8f,
            TextColor      = Color.White,
            TextFont       = Font.ContentBold,
            TextHAlign     = StringHAlign.Right,
            TextVAlign     = StringVAlign.Center,

            TextMonospaced = true,
        };

        private readonly FrameSettings statusFrame = new FrameSettings
        {
            Size   = new Point(64, 26),
            Margin = new Point(2, 2),

            RegularColor      = Palette.Transparent1,
            MouseOverColor    = Palette.Transparent1,
            PendingColor      = Palette.Transparent1,
            ModificationColor = Palette.Transparent1,
            SelectionColor    = Palette.Transparent1,
        };

        private readonly LabelSettings statusLabel = new LabelSettings{
            TextSize       = 0.7f,
            TextColor      = Color.White,
            TextFont       = Font.UiStatistics,
            TextHAlign     = StringHAlign.Center,
            TextVAlign     = StringVAlign.Center,
            TextMonospaced = false,
        };

        public TestItemSettings()
        {
            this.Add("RootFrame", this.rootFrame);

            this.Add("IdFrame", this.idFrame);
            this.Add("IdLabel", this.idLabel);

            this.Add("NameFrame", this.nameFrame);
            this.Add("NameLabel", this.nameLabel);
            this.Add("NameMargin", new Vector2(5, 12) * this.nameLabel.TextSize);

            this.Add("StatusFrame", this.statusFrame);
            this.Add("StatusLabel", this.statusLabel);
        }
    }
}