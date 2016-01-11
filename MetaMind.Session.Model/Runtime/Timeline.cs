namespace MetaMind.Session.Model.Runtime
{
    using System;
    using System.Collections.Generic;

    public class Timeline
    {
        public Timeline(DateTime timeBegin, DateTime timeEnd)
        {
            this.TimeBegin = timeBegin;
            this.TimeEnd   = timeEnd;
        }

        public DateTime TimeBegin { get; }

        public DateTime TimeEnd { get; }

        public TimeSpan Duration => this.TimeEnd - this.TimeBegin;
    }

    public class AwarenessTimeline
    {
        private List<Timeline> timelines;

        public AwarenessTimeline(List<Timeline> timelines)
        {
            if (timelines == null)
            {
                throw new ArgumentNullException(nameof(timelines));
            }

            this.timelines = timelines;
        }

        public AwarenessTimeline()
        {
            this.timelines = new List<Timeline>();
        }
    }
}
