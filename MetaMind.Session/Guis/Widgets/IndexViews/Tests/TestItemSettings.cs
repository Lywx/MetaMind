namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using Engine.Components.Graphics;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Settings;
    using Engine.Entities.Graphics.Fonts;
    using Engine.Services.Loader;
    using Engine.Settings;
    using Microsoft.Xna.Framework;
    using Modules;

    public class TestItemSettings : ItemSettings, IParameterDependant<MMGraphicsSettings>
    {
        private readonly MMViewItemRenderSettings rootFrame;

        private readonly MMViewItemRenderSettings idFrame;

        private readonly LabelSettings idLabel = new LabelSettings
        {
            Size   = 0.7f,
            Color  = Color.White,
            Font   = Font.UiStatistics,
            HAlignment = HoritonalAlignment.Center,
            VAlignment = VerticalAlignment.Center,
        };

        private readonly MMViewItemRenderSettings plusFrame;

        private readonly LabelSettings plusLabel = new LabelSettings
        {
            Size   = 1.0f,
            Color  = Color.White,
            Font   = Font.UiStatistics,
            HAlignment = HoritonalAlignment.Center,
            VAlignment = VerticalAlignment.Center,
        };

        private readonly MMViewItemRenderSettings statusFrame;

        private readonly LabelSettings statusLabel = new LabelSettings{
            Size       = 0.7f,
            Color      = Color.White,
            Font       = Font.UiStatistics,
            HAlignment     = HoritonalAlignment.Center,
            VAlignment     = VerticalAlignment.Center,
            Monospaced = false,
        };

        private readonly MMViewItemRenderSettings statisticsFrame;

        private readonly LabelSettings statisticsLabel = new LabelSettings{
            Size       = 0.7f,
            Color      = Color.White,
            Font       = Font.UiStatistics,
            HAlignment     = HoritonalAlignment.Center,
            VAlignment     = VerticalAlignment.Center,
            Monospaced = false,
        };

        private readonly MMViewItemRenderSettings nameFrame;

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

        private readonly MMViewItemRenderSettings descriptionFrame;

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
            this.LoadParameter(this.EngineGraphics.Settings);

            this.rootFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(this.viewportWidth - TesTMVCSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2)
            };

            this.nameFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(this.viewportWidth - TesTMVCSettings.ViewMargin.X * 2 - 24 - 128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.DimBlue,
                MouseOverColor    = MMPalette.DimBlue,
                PendingColor      = MMPalette.LightBlue,
                ModificationColor = MMPalette.DimBlue,
                SelectionColor    = MMPalette.LightBlue,
            };

            this.descriptionFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(this.viewportWidth - TesTMVCSettings.ViewMargin.X * 2 - 24 - 128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent,
                MouseOverColor    = MMPalette.DimBlue,
                PendingColor      = MMPalette.Transparent80,
                ModificationColor = MMPalette.Transparent,
                SelectionColor    = MMPalette.Transparent80,
            };
            this.idFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(24, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.DarkRed,
                MouseOverColor    = MMPalette.DarkRed,
                PendingColor      = MMPalette.LightYellow,
                ModificationColor = MMPalette.DarkRed,
                SelectionColor    = MMPalette.DarkRed,
            };

            this.plusFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(24, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent20,
                MouseOverColor    = MMPalette.Transparent20,
                PendingColor      = MMPalette.Transparent20,
                ModificationColor = MMPalette.Transparent20,
                SelectionColor    = MMPalette.Transparent20,
            };

            this.statusFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(128, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent20,
                MouseOverColor    = MMPalette.Transparent20,
                PendingColor      = MMPalette.Transparent20,
                ModificationColor = MMPalette.Transparent20,
                SelectionColor    = MMPalette.Transparent20,
            };

            this.statisticsFrame = new MMViewItemRenderSettings
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