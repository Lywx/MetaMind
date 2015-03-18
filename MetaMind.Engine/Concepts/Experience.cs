namespace MetaMind.Engine.Concepts
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [DataContract]
    public class Experience
    {
        public static Experience Zero { get { return new Experience(DateTime.Now, TimeSpan.FromHours(0), DateTime.Now); } }

        #region Time Data

        [DataMember]
        public TimeSpan CertainDuration { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return this.HasEnded
                           ? this.CertainDuration
                           : this.CertainDuration + TimeSpan.FromSeconds((long)((DateTime.Now - this.RecentStartTime).TotalSeconds * this.Accelaration));
            }
        }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public bool HasEnded { get; set; }

        [DataMember]
        public DateTime HistoricalStartTime { get; set; }

        [DataMember]
        public DateTime RecentStartTime { get; set; }

        #endregion Time Data

        #region State Data

        [DataMember]
        public double Accelaration { get; set; }

        #endregion State Data

        #region Constructors

        public Experience()
        {
            this.HistoricalStartTime = DateTime.Now;
            this.RecentStartTime     = DateTime.Now;
            this.Accelaration        = 1f;
            this.HasEnded            = false;
        }

        public Experience(DateTime historicalStartTime, DateTime recentStartTime, TimeSpan certainDuration)
        {
            this.HistoricalStartTime = historicalStartTime;
            this.RecentStartTime     = recentStartTime;
            this.CertainDuration     = certainDuration;
            this.Accelaration        = 1f;
            this.HasEnded            = false;
        }

        public Experience(DateTime historicalStartTime, TimeSpan certainDuration, DateTime endTime)
        {
            this.HistoricalStartTime = historicalStartTime;
            this.EndTime             = endTime;
            this.CertainDuration     = certainDuration;
            this.Accelaration        = 1f;
            this.HasEnded            = true;
        }

        #endregion Constructors

        #region Overloadings

        public static Experience operator -(Experience lhs, TimeSpan rhs)
        {
            lhs.CertainDuration -= rhs;
            return lhs;
        }

        public static Experience operator +(Experience lhs, TimeSpan rhs)
        {
            lhs.CertainDuration += rhs;
            return lhs;
        }

        public static Experience operator -(Experience lhs, Experience rhs)
        {
            Experience exp;
            if (lhs.HasEnded && rhs.HasEnded)
            {
                exp = new Experience(
                    historicalStartTime: lhs.HistoricalStartTime,
                    endTime:             lhs.EndTime,
                    certainDuration:     lhs.CertainDuration - rhs.CertainDuration);
            }
            else if (!lhs.HasEnded)
            {
                exp = new Experience(
                    historicalStartTime: lhs.HistoricalStartTime,
                    recentStartTime:     lhs.RecentStartTime,
                    certainDuration:     lhs.CertainDuration - rhs.CertainDuration);
            }
            else
            {
                throw new InvalidOperationException("At least one experience has to be ended.");
            }

            return exp;
        }

        public static Experience operator +(Experience lhs, Experience rhs)
        {
            Experience exp;
            if (lhs.HasEnded && rhs.HasEnded)
            {
                exp = new Experience(

                    // first starter set start time
                    historicalStartTime: lhs.HistoricalStartTime < rhs.HistoricalStartTime
                            ? lhs.HistoricalStartTime
                            : rhs.HistoricalStartTime,

                    // last ender set end time
                    endTime:         lhs.EndTime > rhs.EndTime ? lhs.EndTime : rhs.EndTime,
                    certainDuration: lhs.CertainDuration + rhs.CertainDuration);
            }
            else if (lhs.HasEnded ^ rhs.HasEnded)
            {
                exp = new Experience(
                    historicalStartTime: lhs.HistoricalStartTime < rhs.HistoricalStartTime ? lhs.HistoricalStartTime : rhs.HistoricalStartTime,
                    recentStartTime:    !lhs.HasEnded ? lhs.RecentStartTime : rhs.RecentStartTime,
                    certainDuration:     lhs.CertainDuration + rhs.CertainDuration);
            }
            else
            {
                throw new InvalidOperationException("At least one experience has to be ended.");
            }

            return exp;
        }

        public override string ToString()
        {
            return ((int)this.Duration.TotalHours).ToString(CultureInfo.InvariantCulture);
        }

        #endregion Overloadings

        #region Operations

        public TimeSpan End()
        {
            this.EndTime = DateTime.Now;

            // TODO: Possible integer type overflow because of zero recent start time
            var recentDuration = TimeSpan.FromMinutes((this.EndTime - this.RecentStartTime).TotalMinutes * this.Accelaration);
            this.CertainDuration = this.CertainDuration + recentDuration;

            this.HasEnded = true;
            return recentDuration;
        }

        public void Abort()
        {
            this.EndTime = DateTime.Now;
            this.HasEnded = true;
        }

        #endregion Operations
    }
}