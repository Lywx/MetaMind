namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Components;
    using Engine.Components.Interop.Event;
    using Events;
    using Microsoft.Xna.Framework;
    using Sessions;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : MMInputableComponent
    {
        private readonly ISynchronization synchronization;

        private readonly string synchronizingFalseCue = "Synchronization False";

        private readonly string synchronizingTrueCue = "Synchronization True";

        private bool     actived;

        private DateTime alertMoment   = DateTime.Now;

        private TimeSpan attentionSpan = TimeSpan.FromSeconds(5);

        public SynchronizationMonitor(MMEngine engine, ISynchronization synchronization)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            this.synchronization = synchronization;

            engine.Components.Add(this);
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            if (DateTime.Now - this.alertMoment > this.attentionSpan)
            {
                var audio = this.Interop.Audio;

                if (this.synchronization.Enabled)
                {
                    audio.PlayMusic(this.synchronizingTrueCue);

                    this.Confirm();
                    this.Alert();
                }
                else
                {
                    audio.PlayMusic(this.synchronizingFalseCue);

                    this.Confirm();
                    this.Alert();
                }
            }
        }

        #endregion

        #region Operations

        private void Alert()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new Event((int)SessionEvent.SyncAlerted, new SynchronizationAlertedEventArgs()));
        }

        private void Confirm()
        {
            this.alertMoment = DateTime.Now;
        }

        public void Start()
        {
            this.actived = true;
        }

        public void TryStart()
        {
            if (!this.actived)
            {
                this.Start();
            }
        }

        public void Stop()
        {
            this.actived = false;
        }

        public void Exit()
        {
            this.Game.Components.Remove(this);
        }

        #endregion
    }
}