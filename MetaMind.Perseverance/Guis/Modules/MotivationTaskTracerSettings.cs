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

    public class MotivationTaskTracerSettings : ICloneable
    {
        public readonly TaskItemSettings ItemSettings = new TaskItemSettings();

        public readonly TaskViewFactory  ViewFactory  = new TaskViewFactory();

        public MotivationTaskTracerSettings()
        {
            this.ViewSettings = new TaskViewSettings
                                    {
                                        ColumnNumDisplay = 1,
                                        ColumnNumMax     = 1,
                                        RowNumMax        = 100,
                                        RowNumDisplay    = GraphicsSettings.Fullscreen ? 13 : 9
                                    };
        }

        public TaskViewSettings ViewSettings { get; private set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}