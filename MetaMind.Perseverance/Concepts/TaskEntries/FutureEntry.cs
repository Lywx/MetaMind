using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [DataContract]
    public class FutureEntry : TaskEntry
    {
        #region Display Data

        public bool IsHighlightedAsParent
        {
            get
            {
                if ( HasChildren &&
                    Children.Values.Any( data => data.IsHighlighted ) )
                    return true;
                return IsHighlighted;
            }
        }

        public bool IsHighlightedAsUpgrade
        {
            get
            {
                if ( HasDowngrades &&
                    Downgrades.Any( data =>
                        data.IsHighlighted
                     || data.IsHighlightedAsUpgrade
                     ) )
                    return true;
                return IsHighlighted;
            }
        }

        public bool IsRunningAsUpgrade
        {
            get
            {
                if ( HasDowngrades &&
                    Downgrades.Any( data => data.IsRunningAsUpgrade ) )
                    return true;
                return IsRunning;
            }
        }

        #endregion Display Data

        #region Future Data

        #region Upgrade and Downgrades

        [DataMember]
        public List<DirectionEntry> Downgrades { get; set; }

        public bool HasDowngrades
        {
            get { return Downgrades != null && Downgrades.Count != 0; }
        }

        #endregion Upgrade and Downgrades

        #region Parent and Children

        private FutureEntry parent;

        [DataMember]
        public SortedList<int, FutureEntry> Children { get; set; }

        public bool HasChildren
        {
            get { return Children != null && Children.Count != 0; }
        }

        public override TaskEntry Parent
        {
            get { return parent; }
            set { parent = ( FutureEntry ) value; }
        }

        #endregion Parent and Children

        #region Level Data

        public int Level
        {
            get
            {
                var level = 0;
                for ( var i = 0 ; i < DirectionEntry.LevelExperience.Length ; ++i )
                {
                    if ( TotalExperience.Duration.TotalMinutes > DirectionEntry.LevelExperience[ i ] )
                        level = i;
                }
                return level;
            }
        }

        public int NextLevel
        {
            get { return Level + 1; }
        }

        public Experience TotalExperience
        {
            get
            {
                var totalExperience = Experience;
                Downgrades.ForEach( downgrade => totalExperience += downgrade.TotalExperience );
                Children.Values.ToList().ForEach( child => totalExperience += child.TotalExperience );
                return totalExperience;
            }
        }

        #endregion Level Data

        #region Extra Data

        [DataMember]
        public DateTime Deadline { get; set; }

        [DataMember]
        public float InfluenceRating { get; set; }

        #endregion Extra Data

        #endregion Future Data

        #region Constructors

        public FutureEntry()
        {
            Symbol = "FT";

            Children = new SortedList<int, FutureEntry>();
            Downgrades = new List<DirectionEntry>();

            SetupFolder();
        }

        #endregion Constructors

        #region Operations

        public override void AddChildData( TaskEntry child )
        {
            child.Parent = this;
            Children.Add( Children.Count, ( FutureEntry ) child );
        }

        protected override void DeleteRelationship()
        {
            // delete children
            foreach ( var child in Children.Values.ToArray() )
            {
                child.Delete();
            }
            Children.Clear();
            // remove data from parent
            if ( HasParent )
            {
                var index = ( ( FutureEntry ) Parent ).Children.IndexOfValue( this );
                ( ( FutureEntry ) Parent ).Children.RemoveAt( index );
                Parent = null;
            }
            // delete downgrades
            foreach ( var child in Downgrades.ToArray() )
            {
                child.Delete();
            }
            Downgrades.Clear();
            // remove data from center data
            Tasklist.RemoveFuture( this );
        }

        public override bool Finish()
        {
            if ( !base.Finish() )
                return false;

            DeleteRelationship();
            // not laid out
            IsLaidOut = false;

            return true;
        }

        #endregion Operations
    }
}