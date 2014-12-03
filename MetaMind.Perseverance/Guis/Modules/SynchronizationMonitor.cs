namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    using MouseKeyboardActivityMonitor;
    using MouseKeyboardActivityMonitor.WinApi;

    public class SynchronizationAlertedEventArgs : EventArgs
    {
    }

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationMonitor : GameComponent
    {
        public TimeSpan AttentionSpan = TimeSpan.FromSeconds(5);

        public string NotSynchronizingCue = "Windows Proximity Connection";
        public string SynchronizingCue    = "Windows Proximity Notification";
        private readonly bool listening;

        private bool     actived;
        private DateTime alertMoment = DateTime.Now;

        private MouseHookListener globalMouseListener;

        public SynchronizationMonitor(Game game, ISynchronization synchronization, bool listening)
            : base(game)
        {
            this.listening = listening;
            this.Synchronization = synchronization;

            this.Game.Components.Add(this);
        }

        public ISynchronization Synchronization { get; set; }

        public void Start()
        {
            // only create mouse hook when listening
            if (this.listening)
            {
                // Note: for an application hook, use the AppHooker class instead
                this.globalMouseListener = new MouseHookListener(new GlobalHooker()) { Enabled = true };

                // Set the event handler
                // recommended to use the Extended handlers, which allow input suppression among other additional information
                this.globalMouseListener.MouseMoveExt += this.AttentionConfirmed;
            }

            this.actived = true;
        }

        public void Stop()
        {
            if (this.listening)
            {
                if (this.globalMouseListener != null)
                {
                    this.globalMouseListener.Dispose();
                }
            }

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

                    this.AttentionConfirmed();
                    this.Alert();
                }
                else
                {
                    GameEngine.AudioManager.PlayMusic(this.NotSynchronizingCue);

                    this.AttentionConfirmed();
                    this.Alert();
                }
            }
        }

        private void Alert()
        {
            var alertedEvent = new EventBase((int)AdventureEventType.SyncAlerted, new SynchronizationAlertedEventArgs());
            GameEngine.EventManager.QueueEvent(alertedEvent);
        }

        private void AttentionConfirmed()
        {
            this.alertMoment = DateTime.Now;
        }
        
        private void AttentionConfirmed(object sender, MouseEventExtArgs e)
        {
            this.AttentionConfirmed();
        }
    }
}