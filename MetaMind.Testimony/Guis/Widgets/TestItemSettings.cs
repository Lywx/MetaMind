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
            Size = new Point(256, 24),
            Margin = new Point(2, 2)
        };

        private readonly FrameSettings idFrame = new FrameSettings
        {
            Size = new Point(24, 24),
            Margin = new Point(2, 2)
        };

        private readonly LabelSettings idLabel = new LabelSettings
        {
            TextSize = 0.7f,
            TextColor = Color.White,
            TextFont = Font.UiStatistics,
            TextHAlign = StringHAlign.Center,
            TextVAlign = StringVAlign.Center,
        };

        private readonly FrameSettings nameFrame = new FrameSettings
        {
            Size = new Point(256, 24),
            Margin = new Point(2, 2),

            RegularColor      = Palette.Transparent2,
            MouseOverColor    = new Color(23, 41, 61, 2),
            PendingColor      = new Color(200, 200, 0, 2),
            ModificationColor = new Color(0, 0, 0, 0),
            SelectionColor    = Palette.LightBlue,
        };

        private readonly LabelSettings nameLabel = new LabelSettings
        {
            TextSize  = 0.7f,
            TextColor = Color.White,
            TextFont  = Font.ContentBold,
            TextHAlign = StringHAlign.Right,
            TextVAlign = StringVAlign.Center,

            TextMonospaced = true,
        };

        private readonly FrameSettings statusFrame = new FrameSettings
        {
            Size = new Point(64, 24),
            Margin = new Point(2, 2),

            RegularColor      = Palette.Transparent2,
            MouseOverColor    = new Color(23, 41, 61, 2),
            PendingColor      = new Color(200, 200, 0, 2),
            ModificationColor = new Color(0, 0, 0, 0),
            SelectionColor    = Palette.LightBlue
        };

        private readonly LabelSettings statusLabel = new LabelSettings{
            TextSize = 0.7f,
            TextColor = Color.White,
            TextFont = Font.ContentBold,
            TextHAlign = StringHAlign.Center,
            TextVAlign = StringVAlign.Center
        };

        public TestItemSettings()
        {
            this.Add("RootFrame", this.rootFrame);

            this.Add("IdFrame", this.idFrame);
            this.Add("IdLabel", this.idLabel);

            this.Add("NameFrame", this.nameFrame);
            this.Add("NameLabel", this.nameLabel);
            this.Add("NameMargin", new Vector2(10, 10) * this.nameLabel.TextSize);

            this.Add("StatusFrame", this.statusFrame);
            this.Add("StatusLabel", this.statusLabel);
        }
    }
}