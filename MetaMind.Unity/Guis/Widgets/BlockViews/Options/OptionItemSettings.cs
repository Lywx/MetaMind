namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using Engine.Components.Fonts;
    using Engine.Components.Graphics;
    using Engine.Guis.Controls.Items.Frames;
    using Engine.Guis.Controls.Items.Settings;
    using Engine.Guis.Controls.Visuals;
    using Engine.Settings.Colors;
    using Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;
    using Modules;

    public class OptionItemSettings : ItemSettings, IParameterLoader<GraphicsSettings>
    {
        private readonly FrameSettings rootFrame;

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

        public OptionItemSettings()
        {
            this.LoadParameter(this.Graphics.Settings);

            this.rootFrame = new FrameSettings
            {
                Size   = new Point(this.viewportWidth - OperationModuleSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2)
            };

            this.nameFrame = new FrameSettings
            {
                Size   = new Point(this.viewportWidth - OperationModuleSettings.ViewMargin.X * 2 - 24, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.DimBlue,
                MouseOverColor    = Palette.DimBlue,
                PendingColor      = Palette.LightBlue,
                ModificationColor = Palette.DimBlue,
                SelectionColor    = Palette.LightBlue,
            };

            this.descriptionFrame = new FrameSettings
            {
                Size   = new Point(this.viewportWidth - OperationModuleSettings.ViewMargin.X * 2, 26),
                Margin = new Point(2, 2),

                RegularColor      = Palette.Transparent,
                MouseOverColor    = Palette.DimBlue,
                PendingColor      = Palette.Transparent80,
                ModificationColor = Palette.Transparent,
                SelectionColor    = Palette.Transparent80,
            };

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

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;
        }
    }
}
