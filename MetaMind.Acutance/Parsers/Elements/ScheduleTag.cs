// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTag.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin  
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

    public enum RepetitionTag
    {
        EveryMoment,

        EveryDay, 

        EveryWeek, 

        // EveryMonth, 

        Unspecified, 
    }

    public struct ScheduleTag
    {
        public readonly DayTag Day;

        public readonly RepetitionTag Repetition;

        public readonly TimeTag Time;

        public ScheduleTag(RepetitionTag repetition, DayTag day, TimeTag time)
        {
            this.Repetition = repetition;
            this.Day         = day;
            this.Time        = time;
        }
    }
}