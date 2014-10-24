using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [DataContract]
    public class DirectionEntry : TaskEntry
    {
        #region Display Data

        public bool IsHighlightedAsDowngrade
        {
            get
            {
                if ( HasUpgrade && (
                    Upgrade.IsHighlighted
                    ) )
                    return true;
                return IsHighlighted;
            }
        }

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
                    Downgrades.Any( data => data.IsHighlighted ) )
                    return true;
                return IsHighlighted;
            }
        }
        public bool IsRunningAsUpgrade
        {
            get
            {
                if ( HasDowngrades &&
                    Downgrades.Any( data => data.IsRunning ) )
                    return true;
                return IsRunning;
            }
        }

        #endregion Display Data

        #region Direction Data

        #region Level Data

        /// <summary>
        /// The exp needed for specific level.
        /// </summary>
        [DataMember]
        public static readonly int[] LevelExperience =
        {
            0,      // level 0 LevelExperience[0]
            30,     // level 1 LevelExperience[1]
            60,     // level 2 LevelExperience[2]
            120,
            180,
            300,
            420,
            660,
            900,
            1380,
            1860,
            2820,
            3780,
            5700,
            7620,
            11460,
            15300,
            22980,
            30660,
            46020,
            61380,
            92100,
            122820,
            184260,
            245700,
            368580,
            491460,
            737220,
            982980,
            1474500, // level 30
            1966020  // level 31
        };

        public int Level
        {
            get
            {
                var level = 0;

                for ( var i = 0 ; i < LevelExperience.Length ; ++i )
                {
                    if ( TotalExperience.Duration.TotalMinutes > LevelExperience[ i ] )
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

        #region Upgrade and Downgrades

        [DataMember]
        public List<QuestionEntry> Downgrades { get; set; }

        public bool HasDowngrades
        {
            get { return Downgrades != null && Downgrades.Count != 0; }
        }

        public bool HasUpgrade
        {
            get { return Upgrade != null; }
        }

        [DataMember]
        public FutureEntry Upgrade { get; set; }

        #endregion Upgrade and Downgrades

        #region Parent and Children

        private DirectionEntry parent;

        [DataMember]
        public SortedList<int, DirectionEntry> Children { get; set; }

        public bool HasChildren
        {
            get { return Children != null && Children.Count != 0; }
        }

        [DataMember]
        public override TaskEntry Parent
        {
            get { return parent; }
            set { parent = ( DirectionEntry ) value; }
        }

        #endregion Parent and Children

        #endregion Direction Data

        #region Constructors

        public DirectionEntry()
        {
            Symbol = "DR";

            Children = new SortedList<int, DirectionEntry>();
            Downgrades = new List<QuestionEntry>();

            SetupFolder();
        }

        #endregion Constructors

        #region Operations

        public override void AddChildData( TaskEntry child )
        {
            child.Parent = this;
            if ( HasUpgrade )
            {
                // one-sided connection with upgrade
                ( ( DirectionEntry ) child ).Upgrade = Upgrade;
            }
            Children.Add( Children.Count, ( DirectionEntry ) child );
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
                var index = ( ( DirectionEntry ) Parent ).Children.IndexOfValue( this );
                ( ( DirectionEntry ) Parent ).Children.RemoveAt( index );
                Parent = null;
            }
            // delete downgrades
            foreach ( var child in Downgrades.ToArray() )
            {
                child.Delete();
            }
            Downgrades.Clear();
            // remove data from upgrade
            if ( HasUpgrade )
            {
                Upgrade.Downgrades.Remove( this );
                Upgrade = null;
            }
            // remove data from center data
            Tasklist.RemoveDirection( this );
        }

        public override bool Finish()
        {
            if ( !base.Finish() )
                return false;

            if ( HasParent )
            {
                Parent.Experience += Experience;
            }
            else if ( HasUpgrade )
            {
                Upgrade.Experience += Experience;
            }

            DeleteRelationship();
            // not laid out
            IsLaidOut = false;

            return true;
        }

        #endregion Operations
    }
}