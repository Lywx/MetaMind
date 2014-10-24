using System;
using System.Collections.Generic;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [Serializable]
    public class Tasklist
    {
        //---------------------------------------------------------------------

        #region Plannning Data

        public List<QuestionEntry>  Questions { get; set; }
        public List<DirectionEntry> Directions { get; set; }
        public List<FutureEntry>    Futures { get; set; }

        #endregion Plannning Data

        //---------------------------------------------------------------------

        #region Constructors

        public Tasklist()
        {
            Questions  = new List<QuestionEntry>();
            Directions = new List<DirectionEntry>();
            Futures    = new List<FutureEntry>();
        }

        #endregion Constructors

        #region Operations

        public QuestionEntry NewQuestion()
        {
            var data = new QuestionEntry();
            Questions.Add( data );
            return data;
        }

        public DirectionEntry NewDirection()
        {
            var data = new DirectionEntry();
            Directions.Add( data );
            return data;
        }

        public FutureEntry NewFuture()
        {
            var data = new FutureEntry();
            Futures.Add( data );
            return data;
        }

        public void RemoveQuestion( QuestionEntry entry )
        {
            Questions.Remove( entry );
        }

        public void RemoveDirection( DirectionEntry entry )
        {
            Directions.Remove( entry );
        }

        public void RemoveFuture( FutureEntry entry )
        {
            Futures.Remove( entry );
        }

        #endregion Operations
    }
}