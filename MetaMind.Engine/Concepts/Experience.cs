namespace MetaMind.Engine.Concepts
{
    using System;
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
                return HasEnded ?
                    CertainDuration :
                    CertainDuration + TimeSpan.FromSeconds((long)((DateTime.Now - RecentStartTime).TotalSeconds * Accelaration));
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
            HistoricalStartTime = DateTime.Now;
            RecentStartTime = DateTime.Now;
            Accelaration = 1f;
            HasEnded = false;
        }

        public Experience(DateTime historicalStartTime, DateTime recentStartTime, TimeSpan certainDuration)
        {
            HistoricalStartTime = historicalStartTime;
            RecentStartTime = recentStartTime;
            CertainDuration = certainDuration;
            Accelaration = 1f;
            HasEnded = false;
        }

        public Experience(DateTime historicalStartTime, TimeSpan certainDuration, DateTime endTime)
        {
            HistoricalStartTime = historicalStartTime;
            EndTime = endTime;
            CertainDuration = certainDuration;
            Accelaration = 1f;
            HasEnded = true;
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
                    endTime: lhs.EndTime,
                    certainDuration: lhs.CertainDuration - rhs.CertainDuration);
            }
            else if (!lhs.HasEnded)
            {
                exp = new Experience(
                    historicalStartTime: lhs.HistoricalStartTime,
                    recentStartTime: lhs.RecentStartTime,
                    certainDuration: lhs.CertainDuration - rhs.CertainDuration);
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
                    // first starter
                    historicalStartTime: lhs.HistoricalStartTime < rhs.HistoricalStartTime ? lhs.HistoricalStartTime : rhs.HistoricalStartTime,
                    // last ender
                    endTime            : lhs.EndTime > rhs.EndTime ? lhs.EndTime : rhs.EndTime, 
                    certainDuration    : lhs.CertainDuration + rhs.CertainDuration);
            }
            else if (lhs.HasEnded ^ rhs.HasEnded)
            {
                exp = new Experience(
                    historicalStartTime: lhs.HistoricalStartTime < rhs.HistoricalStartTime ? lhs.HistoricalStartTime : rhs.HistoricalStartTime,
                    recentStartTime    : !lhs.HasEnded ? lhs.RecentStartTime : rhs.RecentStartTime,
                    certainDuration    : lhs.CertainDuration + rhs.CertainDuration);
            }
            else
            {
                throw new InvalidOperationException("At least one experience has to be ended.");
            }

            return exp;
        }

        public override string ToString()
        {
            return ((int)Duration.TotalHours).ToString();
        }

        #endregion Overloadings

        #region Operations

        public TimeSpan End()
        {
            EndTime = DateTime.Now;
            var recentDuration = TimeSpan.FromSeconds((long)((EndTime - RecentStartTime).TotalSeconds * Accelaration));
            CertainDuration = CertainDuration + recentDuration;
            HasEnded = true;
            return recentDuration;
        }

        #endregion Operations
    }
}