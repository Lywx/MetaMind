// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessBase.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Processes
{
    using Microsoft.Xna.Framework;

    public abstract class ProcessBase : IProcess
    {
        private IProcess child;

        private ProcessState state;

        public IProcess Child
        {
            get
            {
                return this.child;
            }
        }

        public ProcessState State
        {
            get
            {
                return this.state;
            }
        }

        #region Constructors

        protected ProcessBase()
        {
            this.state = ProcessState.Uninitilized;
        }

        #endregion Constructors

        #region Destructors

        ~ProcessBase()
        {
            if (this.child != null)
            {
                this.child.OnAbort();
            }
        }

        #endregion Destructors

        #region Process States

        public bool IsAlive
        {
            get
            {
                return this.state == ProcessState.Running || this.state == ProcessState.Paused;
            }
        }

        public bool IsDead
        {
            get
            {
                return this.state == ProcessState.Succeeded || this.state == ProcessState.Failed
                       || this.state == ProcessState.Aborted;
            }
        }

        public bool IsPaused
        {
            get
            {
                return this.state == ProcessState.Paused;
            }
        }

        public bool IsRemoved
        {
            get
            {
                return this.state == ProcessState.Removed;
            }
        }

        #endregion Process States

        #region Process Transition

        public abstract void OnAbort();

        public abstract void OnFail();

        public virtual void OnInit()
        {
            this.state = ProcessState.Running;
        }

        public abstract void OnSuccess();

        #endregion Process Transition

        #region Process Update and Draw

        public virtual void Draw(GameTime gameTime)
        {
        }

        public abstract void Update(GameTime gameTime);

        #endregion Update and Draw

        #region Process Operations

        public void Abort()
        {
            this.state = ProcessState.Aborted;
        }

        public void AttachChild(IProcess process)
        {
            if (this.child != null)
            {
                this.child.AttachChild(process);
            }
            else
            {
                this.child = process;
            }
        }

        public void Fail()
        {
            this.state = ProcessState.Failed;
        }

        public void Pause()
        {
            if (this.state == ProcessState.Running)
            {
                this.state = ProcessState.Paused;
            }
        }

        public IProcess RemoveChild()
        {
            if (this.child != null)
            {
                var removedChild = this.child;
                this.child = null;
                return removedChild;
            }

            return null;
        }

        public void Resume()
        {
            if (this.state == ProcessState.Paused)
            {
                this.state = ProcessState.Running;
            }
        }

        public void Succeed()
        {
            this.state = ProcessState.Succeeded;
        }

        #endregion Operations
    }
}