using System;
using System.Collections.Generic;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [Serializable]
    public class Tasklist
    {
        //---------------------------------------------------------------------

        #region Plannning entry

        public List<QuestionEntry>  Questions { get; set; }
        public List<DirectionEntry> Directions { get; set; }
        public List<FutureEntry>    Futures { get; set; }

        #endregion Plannning entry

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

        public QuestionEntry CreateQuestion()
        {
            var entry = new QuestionEntry();
            Questions.Add( entry );
            return entry;
        }

        public DirectionEntry CreateDirection()
        {
            var entry = new DirectionEntry();
            Directions.Add( entry );
            return entry;
        }

        public FutureEntry CreateFuture()
        {
            var entry = new FutureEntry();
            Futures.Add( entry );
            return entry;
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