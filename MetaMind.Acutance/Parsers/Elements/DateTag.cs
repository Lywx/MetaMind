// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTag.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    [Serializable]
    public enum DayTag
    {
        Sunday, 

        Monday, 

        Tuesday, 

        Wednesday, 

        Thursday, 

        Friday, 

        Saturday, 

        Unspecified, 
    }

    public enum RepeativityTag
    {
        EveryMoment,

        EveryDay, 

        EveryWeek, 

        // EveryMonth, 

        Unspecified, 
    }

    public struct DateTag
    {
        public readonly DayTag Day;

        public readonly RepeativityTag Repeativity;

        public readonly TimeTag Time;

        public DateTag(RepeativityTag repeativity, DayTag day, TimeTag time)
        {
            this.Repeativity = repeativity;
            this.Day         = day;
            this.Time        = time;
        }
    }
}