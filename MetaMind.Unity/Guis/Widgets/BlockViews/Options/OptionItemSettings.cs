﻿namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using Engine.Component.Graphics;
    using Engine.Component.Graphics.Fonts;
    using Engine.Gui.Control.Item.Frames;
    using Engine.Gui.Control.Item.Settings;
    using Engine.Gui.Control.Visuals;
    using Engine.Setting.Color;
    using Engine.Setting.Loader;
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
            Size   = 0.7f,
            Color  = Color.White,
            Font   = Font.UiStatistics,
            HAlignment = HoritonalAlignment.Center,
            VAlignment = VerticalAlignment.Center,
        };

        private readonly FrameSettings nameFrame;

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

        private readonly FrameSettings descriptionFrame;

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
            this.Add("NameMargin", new Vector2(5, 12) * this.nameLabel.Size);

            this.Add("DescriptionFrame", this.descriptionFrame);
            this.Add("DescriptionLabel", this.descriptionLabel);
            this.Add("DescriptionMargin", new Vector2(5, 12) * this.descriptionLabel.Size);
        }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;
        }
    }
}
