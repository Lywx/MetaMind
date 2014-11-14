// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessBase.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Processes
{
    using Microsoft.Xna.Framework;

    public abstract class ProcessBase : EngineObject, IProcess
    {
        #region Process Data

        private IProcess child;

        private ProcessState state;

        public IProcess Child
        {
            get { return child; }
        }

        public ProcessState State
        {
            get { return state; }
        }

        public bool IsAlive
        {
            get
            {
                return state == ProcessState.Running || 
                       state == ProcessState.Paused;
            }
        }

        public bool IsDead
        {
            get
            {
                return state == ProcessState.Succeeded || 
                       state == ProcessState.Failed || 
                       state == ProcessState.Aborted;
            }
        }

        public bool IsPaused
        {
            get
            {
                return state == ProcessState.Paused;
            }
        }

        public bool IsRemoved
        {
            get
            {
                return state == ProcessState.Removed;
            }
        }

        #endregion Process Data

        #region Constructors

        protected ProcessBase()
        {
            state = ProcessState.Uninitilized;
        }

        #endregion Constructors

        #region Deconstructors

        ~ProcessBase()
        {
            if (child != null)
            {
                child.OnAbort();
            }
        }

        #endregion Deconstructors

        #region Operations

        public void Abort()
        {
            state = ProcessState.Aborted;
        }

        public void AttachChild(IProcess process)
        {
            if (child != null)
            {
                child.AttachChild(process);
            }
            else
            {
                child = process;
            }
        }

        public void Fail()
        {
            state = ProcessState.Failed;
        }

        public void Pause()
        {
            if (state == ProcessState.Running)
            {
                state = ProcessState.Paused;
            }
        }

        public IProcess RemoveChild()
        {
            if (child != null)
            {
                IProcess removedChild = child;
                child = null;
                return removedChild;
            }

            return null;
        }

        public void Resume()
        {
            if (state == ProcessState.Paused)
            {
                state = ProcessState.Running;
            }
        }

        public void Succeed()
        {
            state = ProcessState.Succeeded;
        }

        #endregion Operations

        #region Transition

        public abstract void OnAbort();

        public abstract void OnFail();

        public virtual void OnInit()
        {
            state = ProcessState.Running;
        }

        public abstract void OnSuccess();

        public abstract void OnUpdate(GameTime gameTime);

        #endregion Transition
    }
}