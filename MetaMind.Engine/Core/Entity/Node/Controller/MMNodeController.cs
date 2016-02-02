namespace MetaMind.Engine.Core.Entity.Node.Controller
{
    using System;
    using Common;
    using Control;

    public class MMNodeController : MMControlComponent, IMMNodeController
    {
        public MMNodeController(MMControlManager manager) : base(manager)
        {
            
        }

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

    }
}