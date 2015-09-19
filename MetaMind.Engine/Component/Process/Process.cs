// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Process.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.Process
{
    public abstract class Process : GameEntity, IProcess
    {
        #region Process Data

        private IProcess child;

        private ProcessState state;

        public IProcess Child => this.child;

        public ProcessState State => this.state;

        #endregion

        #region Constructors

        protected Process()
        {
            this.state = ProcessState.Uninitilized;
        }

        #endregion Constructors

        #region Destructors

        ~Process()
        {
            this.Dispose();
        }

        #endregion Destructors

        #region Process States

        public bool IsAlive => this.state == ProcessState.Running || 
                               this.state == ProcessState.Paused;

        public bool IsDead => this.state == ProcessState.Succeeded || 
                              this.state == ProcessState.Failed || 
                              this.state == ProcessState.Aborted;

        public bool IsPaused => this.state == ProcessState.Paused;

        public bool IsRemoved => this.state == ProcessState.Removed;

        #endregion Process States

        #region Process Transition

        public abstract void OnAbort();

        public abstract void OnFail();

        public virtual void OnInit()
        {
            this.state = ProcessState.Running;
        }

        public abstract void OnSucceed();

        #endregion Process Transition

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

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        // Aborted child if its child is not successfully separated out
                        this.child?.OnAbort();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}