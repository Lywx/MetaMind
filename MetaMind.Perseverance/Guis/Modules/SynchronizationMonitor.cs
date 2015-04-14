namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Events;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    using Game = Microsoft.Xna.Framework.Game;
    using GameComponent = Microsoft.Xna.Framework.GameComponent;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : GameComponent
    {
        public TimeSpan AttentionSpan = TimeSpan.FromSeconds(5);

        public string NotSynchronizingCue = "Windows Proximity Connection";
        public string SynchronizingCue    = "Windows Proximity Notification";

        private bool     actived;
        private DateTime alertMoment = DateTime.Now;

        public SynchronizationMonitor(Game game, ISynchronization synchronization)
            : base(game)
        {
            this.Synchronization = synchronization;

            this.Game.Components.Add(this);
        }

        public ISynchronization Synchronization { get; set; }

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
                    GameEngine.AudioManager.PlayMusic(this.SynchronizingCue);

                    this.Confirm();
                    this.Alert();
                }
                else
                {
                    GameEngine.AudioManager.PlayMusic(this.NotSynchronizingCue);

                    this.Confirm();
                    this.Alert();
                }
            }
        }

        private void Alert()
        {
            var alertedEvent = new Event((int)SessionEventType.SyncAlerted, new SynchronizationAlertedEventArgs());
            gameInterop.Event.QueueEvent(alertedEvent);
        }

        private void Confirm()
        {
            this.alertMoment = DateTime.Now;
        }
    }
}