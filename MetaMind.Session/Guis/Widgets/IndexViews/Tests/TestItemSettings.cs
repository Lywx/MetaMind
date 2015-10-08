namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using Engine.Components.Graphics;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Settings;
    using Engine.Gui.Graphics.Fonts;
    using Engine.Services.Loader;
    using Engine.Settings.Color;
    using Microsoft.Xna.Framework;
    using Modules;

    public class TestItemSettings : ItemSettings, IParameterLoader<MMGraphicsSettings>
    {
        private readonly ViewItemVisualSettings rootFrame;

        private readonly ViewItemVisualSettings idFrame;

        private readonly LabelSettings idLabel = new LabelSettings
        {
            Size   = 0.7f,
            Color  = Color.White,
            Font   = Font.UiStatistics,
            HAlignment = HoritonalAlignment.Center,
            VAlignment = VerticalAlignment.Center,
        };

        private readonly ViewItemVisualSettings plusFrame;

        private readonly LabelSettings plusLabel = new LabelSettings
        {
            Size   = 1.0f,
            Color  = Color.White,
            Font   = Font.UiStatistics,
            HAlignment = HoritonalAlignment.Center,
            VAlignment = VerticalAlignment.Center,
        };

        private readonly ViewItemVisualSettings statusFrame;

        private readonly LabelSettings statusLabel = new LabelSettings{
            Size       = 0.7f,
            Color      = Color.White,
            Font       = Font.UiStatistics,
            HAlignment     = HoritonalAlignment.Center,
            VAlignment     = VerticalAlignment.Center,
            Monospaced = false,
        };

        private readonly ViewItemVisualSettings statisticsFrame;

        private readonly LabelSettings statisticsLabel = new LabelSettings{
            Size       = 0.7f,
            Color      = Color.White,
            Font       = Font.UiStatistics,
            HAlignment     = HoritonalAlignment.Center,
            VAlignment     = VerticalAlignment.Center,
            Monospaced = false,
        };

        private readonly ViewItemVisualSettings nameFrame;

        private readonly LabelSettings nameLabel = new LabelSettings
        {
            Size       = 0.8f,
            Color      = Color.White,
            Font       = Font.ContentRegular,
            HAlignment     = HoritonalAlignment.Right,
            VAlignment     = VerticalAlignment.Center,
            Leading    = 26,

            Monospaced = true,
        };

        private readonly ViewItemVisualSettings descriptionFrame;

        private readonly LabelSettings descriptionLabel = new LabelSettings
        {
            Size       = 0.8f,
            Color      = Color.White,
            Font       = Font.ContentBold,
            HAlignment     = HoritonalAlignment.Right,
            VAlignment     = VerticalAlignment.Center,
            Leading    = 26,

            Monospaced = true,
        };

        private int viewportWidth;

        public TestItemSettings()
        {
            this.LoadParameter(this.Graphics.Settings);

            this.rootFrame = new ViewItemVisualSettings
            {
                Size   = new Point(this.viewportWidth - TesTMvcSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2)
            };

            this.nameFrame = new ViewItemVisualSettings
            {
                Size   = new Point(this.viewportWidth - TesTMvcSettings.ViewMargin.X * 2 - 24 - 128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.DimBlue,
                MouseOverColor    = MMPalette.DimBlue,
                PendingColor      = MMPalette.LightBlue,
                ModificationColor = MMPalette.DimBlue,
                SelectionColor    = MMPalette.LightBlue,
            };

            this.descriptionFrame = new ViewItemVisualSettings
            {
                Size   = new Point(this.viewportWidth - TesTMvcSettings.ViewMargin.X * 2 - 24 - 128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent,
                MouseOverColor    = MMPalette.DimBlue,
                PendingColor      = MMPalette.Transparent80,
                ModificationColor = MMPalette.Transparent,
                SelectionColor    = MMPalette.Transparent80,
            };
            this.idFrame = new ViewItemVisualSettings
            {
                Size   = new Point(24, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.DarkRed,
                MouseOverColor    = MMPalette.DarkRed,
                PendingColor      = MMPalette.LightYellow,
                ModificationColor = MMPalette.DarkRed,
                SelectionColor    = MMPalette.DarkRed,
            };

            this.plusFrame = new ViewItemVisualSettings
            {
                Size   = new Point(24, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent20,
                MouseOverColor    = MMPalette.Transparent20,
                PendingColor      = MMPalette.Transparent20,
                ModificationColor = MMPalette.Transparent20,
                SelectionColor    = MMPalette.Transparent20,
            };

            this.statusFrame = new ViewItemVisualSettings
            {
                Size   = new Point(128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent20,
                MouseOverColor    = MMPalette.Transparent20,
                PendingColor      = MMPalette.Transparent20,
                ModificationColor = MMPalette.Transparent20,
                SelectionColor    = MMPalette.Transparent20,
            };

            this.statisticsFrame = new ViewItemVisualSettings
            {
                Size   = new Point(128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent20,
                MouseOverColor    = MMPalette.Transparent20,
                PendingColor      = MMPalette.Transparent20,
                ModificationColor = MMPalette.Transparent20,
                SelectionColor    = MMPalette.Transparent20,
            };

            this.Add("RootFrame", this.rootFrame);

            this.Add("IdFrame", this.idFrame);
            this.Add("IdLabel", this.idLabel);

            this.Add("PlusFrame", this.plusFrame);
            this.Add("PlusLabel", this.plusLabel);

            this.Add("NameFrame", this.nameFrame);
            this.Add("NameLabel", this.nameLabel);
            this.Add("NameMargin", new Vector2(5, 12) * this.nameLabel.Size);

            this.Add("DescriptionFrame", this.descriptionFrame);
            this.Add("DescriptionLabel", this.descriptionLabel);
            this.Add("DescriptionMargin", new Vector2(5, 12) * this.descriptionLabel.Size);

            this.Add("StatusFrame", this.statusFrame);
            this.Add("StatusLabel", this.statusLabel);

            this.Add("StatisticsFrame", this.statisticsFrame);
            this.Add("StatisticsLabel", this.statisticsLabel);
        }

        public void LoadParameter(MMGraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;
        }
    }
}