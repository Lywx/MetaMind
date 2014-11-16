namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Perseverance.Concepts.Cognitions;

    using Microsoft.Xna.Framework;

    using MouseKeyboardActivityMonitor;
    using MouseKeyboardActivityMonitor.WinApi;

    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationHudMonitor : GameComponent
    {
        private readonly ISynchronization synchronization;

        private readonly TimeSpan attentionSpan            = TimeSpan.FromSeconds(5);

        private          DateTime attentionTriggeredMoment = DateTime.Now;

        private MouseHookListener globalMouseListener;

        private bool              actived;
        private bool              listened;

        public SynchronizationHudMonitor(Game game, ISynchronization synchronization, bool listen)
            : base(game)
        {
            this.synchronization = synchronization;
            this.listened = listen;

            this.Game.Components.Add(this);
        }

        public void TryStart()
        {
            if (!this.actived)
            {
                this.Start();
            }
        }

        public void Start()
        {
            // only create mouse hook when listening
            if (listened)
            {
                // Note: for an application hook, use the AppHooker class instead
                this.globalMouseListener = new MouseHookListener(new GlobalHooker()) { Enabled = true };

                // Set the event handler
                // recommended to use the Extended handlers, which allow input suppression among other additional information
                this.globalMouseListener.MouseMoveExt += this.TriggerAttention;
            }

            this.actived = true;
        }

        public void Stop()
        {
            if (listened)
            {
                if (this.globalMouseListener != null)
                {
                    this.globalMouseListener.Dispose();
                }
            }

            this.actived = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.synchronization.Enabled && 
                DateTime.Now - this.attentionTriggeredMoment > this.attentionSpan)
            {
                GameEngine.AudioManager.PlayMusic("Windows Proximity Notification");

                this.attentionTriggeredMoment = DateTime.Now;
            }
            else if (!this.synchronization.Enabled)
            {
                GameEngine.AudioManager.PlayMusic("Windows Proximity Connection");

                this.attentionTriggeredMoment = DateTime.Now;
            }
        }

        private void TriggerAttention(object sender, MouseEventExtArgs e)
        {
            this.attentionTriggeredMoment = DateTime.Now;
        }
    }
}