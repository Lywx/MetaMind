namespace MetaMind.Engine.Concepts
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [DataContract]
    public class SynchronizationSpan
    {
        public static SynchronizationSpan Zero
        {
            get
            {
                return new SynchronizationSpan(DateTime.Now, TimeSpan.FromHours(0), DateTime.Now);
            }
        }

        #region Time Data

        [DataMember]
        public TimeSpan CertainDuration { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return this.IsEnded
                           ? this.CertainDuration
                           : this.CertainDuration + TimeSpan.FromSeconds((long)((DateTime.Now - this.RecentStartTime).TotalSeconds * this.Accelaration));
            }
        }

        [DataMember]
        public DateTime FirstStartTime { get; set; }

        [DataMember]
        public DateTime RecentEndTime { get; set; }
        [DataMember]
        public DateTime RecentStartTime { get; set; }

        #endregion Time Data

        #region State Data

        [DataMember]
        public double Accelaration { get; set; }

        #endregion State Data

        #region Transition Data

        [DataMember]
        private bool IsEnded { get; set; }

        #endregion

        #region Constructors

        public SynchronizationSpan()
            : this(DateTime.Now, DateTime.Now, TimeSpan.Zero)
        {
        }

        public SynchronizationSpan(DateTime firstStartTime, DateTime recentStartTime, TimeSpan certainDuration)
        {
            this.FirstStartTime  = firstStartTime;
            this.RecentStartTime = recentStartTime;

            this.CertainDuration = certainDuration;

            this.Accelaration = 1f;

            this.IsEnded = false;
        }

        private SynchronizationSpan(DateTime firstStartTime, TimeSpan certainDuration, DateTime recentEndTime)
        {
            this.FirstStartTime  = firstStartTime;
            this.RecentEndTime   = recentEndTime;

            this.CertainDuration = certainDuration;

            this.Accelaration = 1f;

            this.IsEnded = true;
        }

        #endregion Constructors

        #region Operator Overloadings

        public static SynchronizationSpan operator -(SynchronizationSpan lhs, TimeSpan rhs)
        {
            lhs.CertainDuration -= rhs;
            return lhs;
        }

        public static SynchronizationSpan operator -(SynchronizationSpan lhs, SynchronizationSpan rhs)
        {
            SynchronizationSpan exp;
            if (lhs.IsEnded && rhs.IsEnded)
            {
                exp = new SynchronizationSpan(
                    firstStartTime:  lhs.FirstStartTime,
                    recentEndTime:   lhs.RecentEndTime,
                    certainDuration: lhs.CertainDuration - rhs.CertainDuration);
            }
            else if (!lhs.IsEnded)
            {
                exp = new SynchronizationSpan(
                    firstStartTime:  lhs.FirstStartTime,
                    recentStartTime: lhs.RecentStartTime,
                    certainDuration: lhs.CertainDuration - rhs.CertainDuration);
            }
            else
            {
                throw new InvalidOperationException("At least one experience has to be ended.");
            }

            return exp;
        }

        public static SynchronizationSpan operator +(SynchronizationSpan lhs, TimeSpan rhs)
        {
            lhs.CertainDuration += rhs;
            return lhs;
        }

        public static SynchronizationSpan operator +(SynchronizationSpan lhs, SynchronizationSpan rhs)
        {
            SynchronizationSpan span;
            if (lhs.IsEnded && rhs.IsEnded)
            {
                span = new SynchronizationSpan(

                    // first starter set start time
                    firstStartTime: lhs.FirstStartTime < rhs.FirstStartTime
                            ? lhs.FirstStartTime
                            : rhs.FirstStartTime,

                    // last ender set end time
                    recentEndTime:   lhs.RecentEndTime > rhs.RecentEndTime ? lhs.RecentEndTime : rhs.RecentEndTime,
                    certainDuration: lhs.CertainDuration + rhs.CertainDuration);
            }
            else if (lhs.IsEnded ^ rhs.IsEnded)
            {
                span = new SynchronizationSpan(
                    firstStartTime:   lhs.FirstStartTime < rhs.FirstStartTime ? lhs.FirstStartTime : rhs.FirstStartTime,
                    recentStartTime: !lhs.IsEnded                             ? lhs.RecentStartTime : rhs.RecentStartTime,
                    certainDuration:  lhs.CertainDuration + rhs.CertainDuration);
            }
            else
            {
                throw new InvalidOperationException("At least one experience has to be ended.");
            }

            return span;
        }

        public override string ToString()
        {
            return ((int)this.Duration.TotalHours).ToString(CultureInfo.InvariantCulture);
        }

        #endregion Overloadings

        #region Operations

        public void Abort()
        {
            this.RecentEndTime = DateTime.Now;
            this.IsEnded = true;
        }

        public TimeSpan End()
        {
            this.Abort();

            // TODO: Possible integer type overflow because of zero recent start time
            var recentDuration = TimeSpan.FromMinutes((this.RecentEndTime - this.RecentStartTime).TotalMinutes * this.Accelaration);
            this.CertainDuration = this.CertainDuration + recentDuration;

            return recentDuration;
        }

        #endregion Operations
    }
}