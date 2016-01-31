namespace MetaMind.Engine.Core.Entity.Node.Model
{
    using System;
    using Actions;
    using Controller;
    using Entities.Graphics;
    using Entity.Common;
    using Graphics;
    using Microsoft.Xna.Framework;
    using View;

    public class MMNode : MMInputtableEntity, IMMNode, IMMNodeInternal
    {
        #region Constructors and Finalizer

        protected internal MMNode()
        {
        }

        #endregion

        #region Dependency 

        private CCScheduler Scheduler => this.GlobalInterop.Process.Scheduler;

        private MMActionManager ActionManager => this.GlobalInterop.Process.ActionManager;

        #endregion

        #region Graphics Data

        public IMMRendererOpacity Opacity => this.Renderer.Opacity;

        public IMMNodeColor Color => this.Renderer.Color;

        #endregion

        #region MVC Data

        public IMMNodeRenderer Renderer { get; protected set; }

        public IMMNodeController Controller { get; protected set; }

        #endregion

        public bool CanFocus => this.Controller.CanFocus;

        public bool HasFocus
        {
            get { return this.Controller.HasFocus; }
            set { this.Controller.HasFocus = value; }
        }

        #region Organization

        public IMMNode Parent { get; set; }

        public MMNodeCollection Children { get; set; } = new MMNodeCollection();

        #endregion

        #region Children Add and Remove

        public virtual void Add(MMNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node == this)
            {
                throw new InvalidOperationException("Can not add node to itself.");
            }

            if (node.Parent != null)
            {
                throw new InvalidOperationException(
                    "Node is already added. It can't be added again.");
            }

            node.Parent = this;

            this.Children  .Add(node);
            this.Controller.Add(node.Controller);
            this.Renderer  .Add(node.Renderer);
        }

        public virtual void Remove(MMNode node, bool disposing)
        {
            if (node == null)
            {
                return;
            }

            if (this.Children.Contains(node))
            {
                this.Detach(node, disposing);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="node">
        /// </param>
        /// <param name="disposing">
        /// If you don't do cleanup, the child's actions will not get removed
        /// and the its ScheduledSelectors dictionary will not get released!
        /// </param>
        private void Detach(MMNode node, bool disposing)
        {
            if (IsRunning)
            {
                node.OnExitTransitionDidStart();
                node.OnExit();
            }

            if (disposing)
            {
                node.Dispose();
            }

            node.Parent = null;

            this.Children  .Remove(node);
            this.Controller.Remove(node.Controller);
            this.Renderer  .Remove(node.Renderer);
        }

        #endregion

        public virtual void OnExit()
        {
            this.Pause();

            IsRunning = false;

            if (this.Children != null
                && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].OnExit();
                }
            }
        }

        #region Load and Unload

        public override void LoadContent()
        {
            base         .LoadContent();
            this.Children.LoadContent();
        }

        public override void UnloadContent()
        {
            this.Children.UnloadContent();
            base         .UnloadContent();
        }
        
        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            base         .Draw(time);
            this.Renderer.Draw(time);

            this.Children.Draw(time);
        }

        public override void BeginDraw(GameTime time)
        {
            base         .BeginDraw(time);
            this.Renderer.BeginDraw(time);

            this.Children.BeginDraw(time);
        }

        public override void EndDraw(GameTime time)
        {
            base         .EndDraw(time);
            this.Renderer.EndDraw(time);

            this.Children.EndDraw(time);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.Children.Update(time);

            base.Update(time);

            // Update cocos interface
            this.Update((float)time.ElapsedGameTime.TotalSeconds);

            this.Controller.Update(time);
            this.Renderer  .Update(time);
        }

        /// <remarks>
        /// You should call Update(float) inside Update(GameTime) and never call
        /// Update(float) from anywhere else.
        /// </remarks>
        /// <param name="dt">
        /// Delta time in seconds. 
        /// </param>
        public virtual void Update(float dt)
        {
        }

        public override void UpdateInput(GameTime time)
        {
            this.Children  .UpdateInput(time);

            this.Controller.UpdateInput(time);
            base           .UpdateInput(time);
        }

        #endregion

        #region Schedule "Update" Operations

        public void Schedule()
        {
            this.Schedule(0);
        }

        public void Schedule(int priority)
        {
            this.Scheduler.Schedule(this, priority, !IsRunning);
        }

        #endregion

        #region

        internal void AttachSchedules()
        {
            if (toBeAddedSchedules != null && toBeAddedSchedules.Count > 0)
            {
                var scheduler = this.Scheduler;
                foreach (var schedule in toBeAddedSchedules)
                {
                    if (schedule.IsPriority)
                    {
                        scheduler.Schedule(
                            schedule.Target,
                            schedule.Priority,
                            schedule.Paused);
                    }
                    else
                    {
                        scheduler.Schedule(
                            schedule.Selector,
                            schedule.Target,
                            schedule.Interval,
                            schedule.Repeat,
                            schedule.Delay,
                            schedule.Paused);}
                }

                toBeAddedSchedules.Clear();
                toBeAddedSchedules = null;
            }
        }

        public void Unschedule()
        {
            this.Scheduler.Unschedule(this);
        }

        #endregion

        #region Schedule "Selector" Operations

        public void Schedule(Action<float> selector)
        {
            this.Schedule(
                selector,
                0.0f,
                CCSchedulePriority.RepeatForever,
                0.0f);
        }

        public void Schedule(Action<float> selector, float interval)
        {
            this.Schedule(
                selector,
                interval,
                CCSchedulePriority.RepeatForever,
                0.0f);
        }

        public void Schedule(Action<float> selector, float interval, uint repeat, float delay)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (interval < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(interval),
                    "Interval must be positive");
            }

            this.Scheduler.Schedule(
                selector,
                this,
                interval,
                repeat,
                delay,
                !IsRunning);
        }

        public void ScheduleOnce(Action<float> selector, float delay)
        {
            this.Schedule(selector, 0.0f, 0, delay);
        }

        public void Unschedule(Action<float> selector)
        {
            if (selector == null)
            {
                return;
            }

            this.Scheduler.Unschedule(selector, this);
        }

        public void UnscheduleAll()
        {
            this.Scheduler.UnscheduleAll(this);
        }

        #endregion 

        public void Resume()
        {
            this.Scheduler    .Resume(this);
            this.ActionManager.ResumeTarget(this);

            if (EventDispatcher != null)
                EventDispatcher.Resume(this);
        }

        public void Pause()
        {
            this.Scheduler    .PauseTarget(this);
            this.ActionManager.PauseTarget(this);

            if (EventDispatcher != null)
            {
                EventDispatcher.Pause(this);
            }
        }

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