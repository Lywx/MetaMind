using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [DataContract]
    public class QuestionEntry : TaskEntry
    {
        #region Display Data

        public bool IsHighlightedAsDowngrade
        {
            get
            {
                if ( HasUpgrade &&
                   ( Upgrade.IsHighlighted ||
                    Upgrade.IsHighlightedAsDowngrade ) )
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

        #endregion Display Data

        #region Question Data

        #region Upgrade and Downgrades

        public bool HasUpgrade
        {
            get { return Upgrade != null; }
        }

        [DataMember]
        public DirectionEntry Upgrade { get; set; }

        #endregion Upgrade and Downgrades

        #region Parent and Children

        private QuestionEntry parent;

        public bool HasChildren
        {
            get { return Children != null && Children.Count != 0; }
        }

        [DataMember]
        public SortedList<int, QuestionEntry> Children { get; set; }

        [DataMember]
        public override TaskEntry Parent
        {
            get { return parent; }
            set { parent = ( QuestionEntry ) value; }
        }

        #endregion Parent and Children

        #region Level Data

        public Experience TotalExperience
        {
            get
            {
                var totalExperience = Experience;
                Children.Values.ToList().ForEach( child => totalExperience += child.TotalExperience );
                return totalExperience;
            }
        }

        #endregion Level Data

        #endregion Question Data

        #region Constructors

        public QuestionEntry()
        {
            Symbol = "QS";
            Children = new SortedList<int, QuestionEntry>();

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
                ( ( QuestionEntry ) child ).Upgrade = Upgrade;
            }
            Children.Add( Children.Count, ( QuestionEntry ) child );
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
                var index = ( ( QuestionEntry ) Parent ).Children.IndexOfValue( this );
                ( ( QuestionEntry ) Parent ).Children.RemoveAt( index );
                Parent = null;
            }
            // remove data from upgrade
            if ( HasUpgrade )
            {
                Upgrade.Downgrades.Remove( this );
                Upgrade = null;
            }
            // remove data from center data
            Tasklist.RemoveQuestion( this );
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