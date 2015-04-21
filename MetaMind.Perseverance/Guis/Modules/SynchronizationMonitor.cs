namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Events;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : GameEntity
    {
        #region Cues

        public string SynchronizingFalseCue = "Windows Proximity Connection";

        public string SynchronizingTrueCue = "Windows Proximity Notification";

        #endregion

        public TimeSpan AttentionSpan = TimeSpan.FromSeconds(5);
        
        private bool     actived;
        private DateTime alertMoment = DateTime.Now;

        #region Dependency

        public ISynchronization Synchronization { get; set; }

        #endregion

        public SynchronizationMonitor(ISynchronization synchronization)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Synchronization = synchronization;
        }

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
            if (DateTime.Now - this.alertMoment > this.AttentionSpan)
            {
                if (this.Synchronization.Enabled)
                {
                    this.Interop.Audio.PlayMusic(this.SynchronizingTrueCue);

                    this.Confirm();
                    this.Alert();
                }
                else
                {
                    this.Interop.Audio.PlayMusic(this.SynchronizingFalseCue);

                    this.Confirm();
                    this.Alert();
                }
            }
        }

        private void Alert()
        {
            this.Interop.Event.QueueEvent(new Event((int)SessionEventType.SyncAlerted, new SynchronizationAlertedEventArgs()));
        }

        private void Confirm()
        {
            this.alertMoment = DateTime.Now;
        }
    }
}