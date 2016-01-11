namespace MetaMind.Engine.Components.Interop.Process
{
    using Entities.Bases;

    public abstract class MMProcess : MMEntity, IMMProcess
    {
        #region Constructors and Finalizer 

        protected MMProcess()
        {
            this.State = ProcessState.Uninitilized;
        }

        ~MMProcess()
        {
            this.Dispose();
        }

        #endregion

        #region Process Data

        public IMMProcess Child { get; private set; }

        public ProcessState State { get; private set; }

        #endregion

        #region Process States

        public bool IsAlive => this.State == ProcessState.Running ||
                               this.State == ProcessState.Paused;

        public bool IsDead => this.State == ProcessState.Succeeded ||
                              this.State == ProcessState.Failed ||
                              this.State == ProcessState.Aborted;

        public bool IsPaused => this.State == ProcessState.Paused;

        public bool IsRemoved => this.State == ProcessState.Removed;

        #endregion Process States

        #region Process Transition

        public abstract void OnAbort();

        public abstract void OnFail();

        public virtual void OnInit()
        {
            this.State = ProcessState.Running;
        }

        public abstract void OnSucceed();

        #endregion Process Transition

        #region Process Operations

        public void Abort()
        {
            this.State = ProcessState.Aborted;
        }

        public void AttachChild(IMMProcess process)
        {
            if (this.Child != null)
            {
                this.Child.AttachChild(process);
            }
            else
            {
                this.Child = process;
            }
        }

        public void Fail()
        {
            this.State = ProcessState.Failed;
        }

        public void Pause()
        {
            if (this.State == ProcessState.Running)
            {
                this.State = ProcessState.Paused;
            }
        }

        public IMMProcess RemoveChild()
        {
            if (this.Child != null)
            {
                var removedChild = this.Child;
                this.Child = null;
                return removedChild;
            }

            return null;
        }

        public void Resume()
        {
            if (this.State == ProcessState.Paused)
            {
                this.State = ProcessState.Running;
            }
        }

        public void Succeed()
        {
            this.State = ProcessState.Succeeded;
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
                        this.Child?.OnAbort();
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
