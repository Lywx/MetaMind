namespace MetaMind.Runtime.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : GameEntity
    {
        #region Cues

        private string SynchronizingFalseCue = "Windows Proximity Connection";

        private string SynchronizingTrueCue  = "Windows Proximity Notification";

        #endregion

        private bool     actived;
        private DateTime alertMoment   = DateTime.Now;
        private TimeSpan attentionSpan = TimeSpan.FromSeconds(5);

        public SynchronizationMonitor(ISynchronization synchronization)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Synchronization = synchronization;
        }

        #region Dependency

        private ISynchronization Synchronization { get; set; }

        #endregion

        public void Start()
        {
            this.actived = true;
        }

        public void Stop()
        {
            this.actived = false;
        }

        public void TryStart()
        {
            if (!this.actived)
            {
                this.Start();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (DateTime.Now - this.alertMoment > this.attentionSpan)
            {
                var audio = this.GameInterop.Audio;

                if (this.Synchronization.Enabled)
                {
                    audio.PlayMusic(this.SynchronizingTrueCue);

                    this.Confirm();
                    this.Alert();
                }
                else
                {
                    audio.PlayMusic(this.SynchronizingFalseCue);

                    this.Confirm();
                    this.Alert();
                }
            }
        }

        private void Alert()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncAlerted, new SynchronizationAlertedEventArgs()));
        }

        private void Confirm()
        {
            this.alertMoment = DateTime.Now;
        }
    }
}