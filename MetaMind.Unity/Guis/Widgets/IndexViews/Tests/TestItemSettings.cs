namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Components.Fonts;
    using Engine.Components.Graphics;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Settings;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Settings.Colors;
    using Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;
    using Modules;

    public class TestItemSettings : ItemSettings, IParameterLoader<GraphicsSettings>
    {
        private readonly FrameSettings rootFrame;

        private readonly FrameSettings idFrame;

        private readonly LabelSettings idLabel = new LabelSettings
        {
            TextSize   = 0.7f,
            TextColor  = Color.White,
            TextFont   = Font.UiStatistics,
            TextHAlign = StringHAlign.Center,
            TextVAlign = StringVAlign.Center,
        };

        private readonly FrameSettings plusFrame;

        private readonly LabelSettings plusLabel = new LabelSettings
        {
            TextSize   = 1.0f,
            TextColor  = Color.White,
            TextFont   = Font.UiStatistics,
            TextHAlign = StringHAlign.Center,
            TextVAlign = StringVAlign.Center,
        };

        private readonly FrameSettings statusFrame;

        private readonly LabelSettings statusLabel = new LabelSettings{
            TextSize       = 0.7f,
            TextColor      = Color.White,
            TextFont       = Font.UiStatistics,
            TextHAlign     = StringHAlign.Center,
            TextVAlign     = StringVAlign.Center,
            TextMonospaced = false,
        };

        private readonly FrameSettings statisticsFrame;

        private readonly LabelSettings statisticsLabel = new LabelSettings{
            TextSize       = 0.7f,
            TextColor      = Color.White,
            TextFont       = Font.UiStatistics,
            TextHAlign     = StringHAlign.Center,
            TextVAlign     = StringVAlign.Center,
            TextMonospaced = false,
        };

        private readonly FrameSettings nameFrame;

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

        private readonly FrameSettings descriptionFrame;

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

        private int viewportWidth;

        public TestItemSettings()
        {
            this.LoadParameter(this.GameGraphics.Settings);

            this.rootFrame = new FrameSettings
            {
                Size   = new Point(this.viewportWidth - TestModuleSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2)
            };

            this.nameFrame = new FrameSettings
            {
                Size   = new Point(this.viewportWidth - TestModuleSettings.ViewMargin.X * 2 - 24 - 128, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.DimBlue,
                MouseOverColor    = Palette.DimBlue,
                PendingColor      = Palette.LightBlue,
                ModificationColor = Palette.DimBlue,
                SelectionColor    = Palette.LightBlue,
            };

            this.descriptionFrame = new FrameSettings
            {
                Size   = new Point(this.viewportWidth - TestModuleSettings.ViewMargin.X * 2 - 24 - 128, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.Transparent,
                MouseOverColor    = Palette.DimBlue,
                PendingColor      = Palette.Transparent80,
                ModificationColor = Palette.Transparent,
                SelectionColor    = Palette.Transparent80,
            };
            this.idFrame = new FrameSettings
            {
                Size   = new Point(24, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.DarkRed,
                MouseOverColor    = Palette.DarkRed,
                PendingColor      = Palette.LightYellow,
                ModificationColor = Palette.DarkRed,
                SelectionColor    = Palette.DarkRed,
            };

            this.plusFrame = new FrameSettings
            {
                Size   = new Point(24, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.Transparent20,
                MouseOverColor    = Palette.Transparent20,
                PendingColor      = Palette.Transparent20,
                ModificationColor = Palette.Transparent20,
                SelectionColor    = Palette.Transparent20,
            };

            this.statusFrame = new FrameSettings
            {
                Size   = new Point(128, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.Transparent20,
                MouseOverColor    = Palette.Transparent20,
                PendingColor      = Palette.Transparent20,
                ModificationColor = Palette.Transparent20,
                SelectionColor    = Palette.Transparent20,
            };

            this.statisticsFrame = new FrameSettings
            {
                Size   = new Point(128, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.Transparent20,
                MouseOverColor    = Palette.Transparent20,
                PendingColor      = Palette.Transparent20,
                ModificationColor = Palette.Transparent20,
                SelectionColor    = Palette.Transparent20,
            };

            this.Add("RootFrame", this.rootFrame);

            this.Add("IdFrame", this.idFrame);
            this.Add("IdLabel", this.idLabel);

            this.Add("PlusFrame", this.plusFrame);
            this.Add("PlusLabel", this.plusLabel);

            this.Add("NameFrame", this.nameFrame);
            this.Add("NameLabel", this.nameLabel);
            this.Add("NameMargin", new Vector2(5, 12) * this.nameLabel.TextSize);

            this.Add("DescriptionFrame", this.descriptionFrame);
            this.Add("DescriptionLabel", this.descriptionLabel);
            this.Add("DescriptionMargin", new Vector2(5, 12) * this.descriptionLabel.TextSize);

            this.Add("StatusFrame", this.statusFrame);
            this.Add("StatusLabel", this.statusLabel);

            this.Add("StatisticsFrame", this.statisticsFrame);
            this.Add("StatisticsLabel", this.statisticsLabel);
        }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;
        }
    }
}