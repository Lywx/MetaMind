namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    public enum CommandRepetion
    {
        EveryMoment,

        EveryDay,

        EveryWeek,

        // EveryMonth,

        Never,
    }

    [DataContract]
    public class CommandTimerWithDate : CommandTimer
    {
        public CommandTimerWithDate(DateTime date, CommandRepetion repetion)
        {
            this.Date        = date;
            this.Repetion = repetion;
        }

        public override bool IsTransiting
        {
            get
            {
                switch (this.Repetion)
                {
                    // TODO: Not implemented properly with parser because day of week cannot determine day in month
                    // case CommandRepetion.EveryMonth:
                    //     return this.SameMonthDay && this.SameTime;

                    case CommandRepetion.EveryWeek:
                        return this.SameWeekDay && this.SameTime;

                    case CommandRepetion.EveryDay:
                        return this.SameTime;

                    case CommandRepetion.EveryMoment:
                        return this.SameTime;

                    case CommandRepetion.Never:
                        return this.SameTime;

                    default:
                        return false;
                }
            }
        }

        public override bool IsAutoReseting
        {
            get { return false; }
        }

        [DataMember]
        private DateTime Date { get; set; }

        private CommandRepetion Repetion { get; set; }

        private bool SameMonthDay
        {
            get
            {
                return DateTime.Now.Day == this.Date.Day;
            }
        }

        private bool SameTime
        {
            get
            {
                var delta = DateTime.Now.TimeOfDay - new TimeSpan(0, this.Date.Hour, this.Date.Minute, this.Date.Second);
                return delta.Duration() < TimeSpan.FromSeconds(0.1);
            }
        }

        private bool SameWeekDay
        {
            get { return DateTime.Now.DayOfWeek == this.Date.DayOfWeek; }
        }

        public override void Reset()
        {
            this.SynchronizationSpan = SynchronizationSpan.Zero;
        }

        public override void Update()
        {
            this.SynchronizationSpan.CertainDuration = DateTime.Now.TimeOfDay - this.Date.TimeOfDay;
        }
    }
}