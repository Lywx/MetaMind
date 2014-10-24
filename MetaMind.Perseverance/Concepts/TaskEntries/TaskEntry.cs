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
        #region Data Source

        protected Tasklist Tasklist { get { return Perseverance.Adventure.Tasklist; } }

        #endregion Data Source

        #region Display State

        public bool IsLaidOut { get; set; }

        public bool IsHighlighted { get; set; }

        public bool IsHighlightedAsChild
        {
            get
            {
                if ( HasParent &&
                    Parent.IsHighlighted )
                    return true;
                return IsHighlighted;
            }
        }

        [DataMember]
        public bool IsRunning { get; set; }

        #endregion Display State

        #region Folder Data

        [DataMember]
        public int GlobalId { get; set; }

        [DataMember]
        public string Symbol { get; set; }

        #endregion Folder Data

        #region Item Data

        #region Parent

        [DataMember]
        public virtual TaskEntry Parent { get; set; }

        public bool HasParent
        {
            get { return Parent != null; }
        }

        #endregion Parent

        #region Concepts

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Experience Experience { get; set; }

        #endregion Concepts

        #endregion Item Data

        #region Constructors

        protected TaskEntry()
        {
            // empty name
            Name = string.Empty;
            // zero experience
            Experience = new Experience( DateTime.Now, TimeSpan.FromHours( 0 ), DateTime.Now );
        }

        #endregion Constructors

        #region Initialization

        protected void SetupFolder()
        {
            GlobalId = FolderManager.NextID;
            Folder = new Folder( Symbol, GlobalId, Name );
            Folder.Enable();
        }

        #endregion Initialization

        #region Operations

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
            // remove folder
            Folder.Delete();
        }

        protected abstract void DeleteRelationship();

        public virtual bool Finish()
        {
            // move folder
            if ( Folder.MoveToRepository() )
            {
                return true;
            }

            MessageManager.PopMessages( "Repository is not ready." );
            return false;
        }

        #endregion Operations
    }
}