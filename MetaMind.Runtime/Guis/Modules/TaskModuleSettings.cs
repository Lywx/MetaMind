// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskModuleSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Runtime.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class TaskModuleSettings : ICloneable
    {
        public readonly TaskItemSettings ItemSettings = new TaskItemSettings();

        public readonly TaskViewFactory  ViewFactory  = new TaskViewFactory();

        public readonly TaskViewSettings ViewSettings;

        public TaskModuleSettings(Point start)
        {
            this.ViewSettings = new TaskViewSettings(start)
                                    {
                                        ColumnNumDisplay = 1,
                                        ColumnNumMax     = 1,

                                        RowNumDisplay = GameEngine.GraphicsSettings.IsFullscreen ? 13 : 9,
                                        RowNumMax        = 100,

                                        PointMargin = new Point(
                                            this.ItemSettings.NameFrameSize.X,
                                            this.ItemSettings.NameFrameSize.Y + this.ItemSettings.IdFrameSize.Y),
                                    };
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}