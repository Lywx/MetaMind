namespace MetaMind.Runtime.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : GameComponent
    {
        #region Cues

        private string SynchronizingFalseCue = "Windows Proximity Connection";

        private string SynchronizingTrueCue  = "Windows Proximity Notification";

        #endregion

        private bool     actived;
        private DateTime alertMoment   = DateTime.Now;
        private TimeSpan attentionSpan = TimeSpan.FromSeconds(5);

        public SynchronizationMonitor(GameEngine engine, ISynchronization synchronization)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.GameInterop = engine.Interop;
            this.Game.Components.Add(this);
            
            this.Synchronization = synchronization;
        }

        #region Dependency

        private IGameInteropService GameInterop { get; set; }

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

        public void Exit()
        {
            this.Game.Components.Remove(this);
        }
    }
}