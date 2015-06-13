namespace MetaMind.Testimony.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Services;
    using MetaMind.Testimony.Concepts.Synchronizations;
    using MetaMind.Testimony.Events;
    using MetaMind.Testimony.Sessions;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : GameComponent
    {
        private readonly IGameInteropService interop;

        private readonly ISynchronization synchronization;

        private readonly string synchronizingFalseCue = "Synchronization False";

        private readonly string synchronizingTrueCue = "Synchronization True";

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

            this.synchronization = synchronization;

            this.interop = engine.Interop;

            engine.Components.Add(this);
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            if (DateTime.Now - this.alertMoment > this.attentionSpan)
            {
                var audio = this.interop.Audio;

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
            var @event = this.interop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncAlerted, new SynchronizationAlertedEventArgs()));
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