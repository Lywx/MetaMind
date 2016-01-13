namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using Engine.Components.Graphics;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Settings;
    using Engine.Entities.Graphics.Fonts;
    using Engine.Services.IO;
    using Engine.Settings;
    using Microsoft.Xna.Framework;
    using Modules;

    public class OptionItemSettings : ItemSettings, IMMParameterDependant<MMGraphicsSettings>
    {
        private readonly MMViewItemRenderSettings rootFrame;

        private readonly MMViewItemRenderSettings idFrame = new MMViewItemRenderSettings
        {
            Size   = new Point(24, 26),
            Margin = new Point(2, 2),

            RegularColor      = MMPalette.DarkRed,
            MouseOverColor    = MMPalette.DarkRed,
            PendingColor      = MMPalette.LightYellow,
            ModificationColor = MMPalette.DarkRed,
            SelectionColor    = MMPalette.DarkRed,
        };

        private readonly LabelSettings idLabel = new LabelSettings
        {
            Size   = 0.7f,
            Color  = Color.White,
            Font   = Font.UiStatistics,
            HAlignment = HoritonalAlignment.Center,
            VAlignment = VerticalAlignment.Center,
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

        public OptionItemSettings()
        {
            this.LoadParameter(this.EngineGraphics.Settings);

            this.rootFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(this.viewportWidth - OperationModuleSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2)
            };

            this.nameFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(this.viewportWidth - OperationModuleSettings.ViewMargin.X * 2 - 24, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.DimBlue,
                MouseOverColor    = MMPalette.DimBlue,
                PendingColor      = MMPalette.LightBlue,
                ModificationColor = MMPalette.DimBlue,
                SelectionColor    = MMPalette.LightBlue,
            };

            this.descriptionFrame = new MMViewItemRenderSettings
            {
                Size   = new Point(this.viewportWidth - OperationModuleSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2),

                RegularColor      = MMPalette.Transparent,
                MouseOverColor    = MMPalette.DimBlue,
                PendingColor      = MMPalette.Transparent80,
                ModificationColor = MMPalette.Transparent,
                SelectionColor    = MMPalette.Transparent80,
            };

            this.Add("RootFrame", this.rootFrame);

            this.Add("IdFrame", this.idFrame);
            this.Add("IdLabel", this.idLabel);

            this.Add("NameFrame", this.nameFrame);
            this.Add("NameLabel", this.nameLabel);
            this.Add("NameMargin", new Vector2(5, 12) * this.nameLabel.Size);

            this.Add("DescriptionFrame", this.descriptionFrame);
            this.Add("DescriptionLabel", this.descriptionLabel);
            this.Add("DescriptionMargin", new Vector2(5, 12) * this.descriptionLabel.Size);
        }

        public void LoadParameter(MMGraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;
        }
    }
}
