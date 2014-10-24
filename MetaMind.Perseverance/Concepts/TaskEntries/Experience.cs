using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [DataContract]
    public class Experience
    {
        #region Time Data

        [DataMember]
        public TimeSpan CertainDuration { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return HasEnded ?
                    CertainDuration :
                    CertainDuration + TimeSpan.FromSeconds( ( long ) ( ( DateTime.Now - RecentStartTime ).TotalSeconds * Accelaration ) );
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

        public Experience( DateTime historicalStartTime, DateTime recentStartTime, TimeSpan certainDuration )
        {
            HistoricalStartTime = historicalStartTime;
            RecentStartTime = recentStartTime;
            CertainDuration = certainDuration;
            Accelaration = 1f;
            HasEnded = false;
        }

        public Experience( DateTime historicalStartTime, TimeSpan certainDuration, DateTime endTime )
        {
            HistoricalStartTime = historicalStartTime;
            EndTime = endTime;
            CertainDuration = certainDuration;
            Accelaration = 1f;
            HasEnded = true;
        }

        #endregion Constructors

        #region Overloadings

        public static Experience operator -( Experience expX, Experience expY )
        {
            Experience exp;
            if ( expX.HasEnded && expY.HasEnded )
            {
                exp = new Experience(
                    historicalStartTime: expX.HistoricalStartTime,
                    endTime: expX.EndTime,
                    certainDuration: expX.CertainDuration - expY.CertainDuration );
            }
            else if ( !expX.HasEnded )
            {
                exp = new Experience(
                    historicalStartTime: expX.HistoricalStartTime,
                    recentStartTime: expX.RecentStartTime,
                    certainDuration: expX.CertainDuration - expY.CertainDuration );
            }
            else
                throw new InvalidOperationException( "At least one experience has to be ended." );

            return exp;
        }

        public static Experience operator +( Experience expX, Experience expY )
        {
            Experience exp;
            if ( expX.HasEnded && expY.HasEnded )
            {
                exp = new Experience(
                historicalStartTime: expX.HistoricalStartTime < expY.HistoricalStartTime ? expX.HistoricalStartTime : expY.HistoricalStartTime, // first starter
                endTime: expX.EndTime > expY.EndTime ? expX.EndTime : expY.EndTime, // last ender
                certainDuration: expX.CertainDuration + expY.CertainDuration );
            }
            else if ( expX.HasEnded ^ expY.HasEnded )
            {
                exp = new Experience(
                historicalStartTime: expX.HistoricalStartTime < expY.HistoricalStartTime ? expX.HistoricalStartTime : expY.HistoricalStartTime,
                recentStartTime: !expX.HasEnded ? expX.RecentStartTime : expY.RecentStartTime,
                certainDuration: expX.CertainDuration + expY.CertainDuration );
            }
            else
                throw new InvalidOperationException( "At least one experience has to be ended." );

            return exp;
        }

        #endregion Overloadings

        #region Operations

        public TimeSpan End()
        {
            EndTime = DateTime.Now;
            var recentDuration = TimeSpan.FromSeconds( ( long ) ( ( EndTime - RecentStartTime ).TotalSeconds * Accelaration ) );
            CertainDuration = CertainDuration + recentDuration;
            HasEnded = true;
            return recentDuration;
        }

        #endregion Operations
    }
}