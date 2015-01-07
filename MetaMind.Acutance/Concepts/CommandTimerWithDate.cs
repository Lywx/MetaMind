namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Concepts;

    public enum CommandRepeativity
    {
        EveryDay,

        EveryWeek,

        // EveryMonth,
    }

    [DataContract]
    public class CommandTimerWithDate : CommandTimer
    {
        public CommandTimerWithDate(DateTime date, CommandRepeativity repeativity)
        {
            this.Date        = date;
            this.Repeativity = repeativity;
        }

        public override bool Transiting
        {
            get
            {
                switch (this.Repeativity)
                {
                    // TODO: Not implemented properly with parser because day of week cannot determine day in month
                    // case CommandRepeativity.EveryMonth:
                    //     return this.SameMonthDay && this.SameTime;

                    case CommandRepeativity.EveryWeek:
                        return this.SameWeekDay && this.SameTime;

                    case CommandRepeativity.EveryDay:
                        return this.SameTime;

                    default:
                        return false;
                }
            }
        }

        [DataMember]
        private DateTime Date { get; set; }

        private CommandRepeativity Repeativity { get; set; }

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
                return delta < TimeSpan.FromSeconds(1);
            }
        }

        private bool SameWeekDay
        {
            get { return DateTime.Now.DayOfWeek == this.Date.DayOfWeek; }
        }

        public override void Reset()
        {
            this.Experience = Experience.Zero;
        }

        public override void Update()
        {
            this.Experience.CertainDuration = DateTime.Now.TimeOfDay - this.Date.TimeOfDay;
        }
    }
}