// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationTaskTracerSettings.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    public class MotivationTaskTracerSettings : ICloneable
    {
        public readonly TaskItemSettings ItemSettings = new TaskItemSettings();

        public readonly TaskViewFactory  ViewFactory  = new TaskViewFactory();
        public readonly TaskViewSettings ViewSettings = new TaskViewSettings
                                                            {
                                                                ColumnNumDisplay = 1, 
                                                                ColumnNumMax     = 1, 
                                                                RowNumDisplay    = 9, 
                                                                RowNumMax        = 100, 
                                                            };

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}