﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeTag.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    public struct TimeTag
    {
        public readonly int Hours;
        public readonly int Minutes;
        public readonly int Seconds;

        public TimeTag(int hours, int minutes, int senconds)
        {
            this.Hours   = hours;
            this.Minutes = minutes;
            this.Seconds = senconds;
        }

        public TimeTag(string hours, string minutes, string seconds)
            : this(int.Parse(hours), int.Parse(minutes), int.Parse(seconds))
        {
        }

        public static TimeTag Zero
        {
            get { return new TimeTag(); }
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(this.Hours, this.Minutes, this.Seconds);
        }
    }
}