// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationTaskTracerSettings.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MotivationTaskTracerSettings : ICloneable
    {
        public readonly TaskItemSettings ItemSettings = new TaskItemSettings();

        public readonly TaskViewFactory  ViewFactory  = new TaskViewFactory();

        public readonly TaskViewSettings ViewSettings;

        public MotivationTaskTracerSettings()
        {
            this.ViewSettings = new TaskViewSettings
                                    {
                                        ColumnNumDisplay = 1,
                                        ColumnNumMax     = 1,

                                        RowNumDisplay    = GraphicsSettings.Fullscreen ? 13 : 9,
                                        RowNumMax        = 100,

                                        RootMargin = new Point(
                                            this.ItemSettings.NameFrameSize.X,
                                            this.ItemSettings.NameFrameSize.Y + this.ItemSettings.IdFrameSize.Y)
                                    };
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}