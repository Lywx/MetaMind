using System;
using System.Runtime.Serialization;
using MetaMind.Engine;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [DataContract,
     KnownType( typeof( QuestionEntry ) ),
     KnownType( typeof( DirectionEntry ) ),
     KnownType( typeof( FutureEntry ) )]
    public abstract class TaskEntry : EngineObject
    {

        public bool IsLaidOut { get; set; }

        public bool IsHighlighted { get; set; }

        public bool IsHighlightedAsChild
        {
            get
            {
                if ( HasParent && Parent.IsHighlighted )
                    return true;
                return IsHighlighted;
            }
        }

        [DataMember]
        public bool IsRunning { get; set; }

        [DataMember]
        public int GlobalId { get; set; }

        [DataMember]
        public string Symbol { get; set; }


        [DataMember]
        public virtual TaskEntry Parent { get; set; }

        public bool HasParent
        {
            get { return Parent != null; }
        }

        [DataMember] public string Name;

        [DataMember] public Experience Experience; 

        protected TaskEntry()
        {
            Name       = string.Empty;
            Experience = new Experience( DateTime.Now, TimeSpan.FromHours( 0 ), DateTime.Now );
        }

        public abstract void AddChildData( TaskEntry child );

        /// <summary>
        /// Deletes this instance.
        /// Remove folder and change data status.
        /// </summary>
        public virtual void Delete()
        {
            DeleteRelationship();
            // not laid out
            IsLaidOut = false;
        }

        protected abstract void DeleteRelationship();

        public virtual bool Finish()
        {
            MessageManager.PopMessages( "Repository is not ready." );
            return false;
        }
    }
}