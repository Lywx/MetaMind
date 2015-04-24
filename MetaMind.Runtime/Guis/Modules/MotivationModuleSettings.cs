// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationModuleSettings.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Guis.Modules
{
    using System;

    using MetaMind.Runtime.Guis.Widgets;

    public class MotivationModuleSettings : ICloneable
    {
        public MotivationModuleSettings()
        {
        }

        public MotivationModuleSettings(MotivationViewSettings viewSettings)
        {
            this.IntelligenceViewSettings = viewSettings;
        }

        public MotivationViewSettings IntelligenceViewSettings { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}