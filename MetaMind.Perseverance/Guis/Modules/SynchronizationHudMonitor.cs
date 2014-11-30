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

        private bool              isActived;


        public SynchronizationHudMonitor(Game game, ISynchronization synchronization)
            : base(game)
        {
            this.synchronization = synchronization;

            this.Game.Components.Add(this);
        }

        public void TryStart()
        {
            if (!this.isActived)
            {
                this.Start();
            }
        }

        public void Start()
        {
            // Note: for an application hook, use the AppHooker class instead
            this.globalMouseListener = new MouseHookListener(new GlobalHooker()) { Enabled = true };

            // Set the event handler
            // recommended to use the Extended handlers, which allow input suppression among other additional information
            this.globalMouseListener.MouseMoveExt += this.TriggerAttention;

            this.isActived = true;
        }

        public void Stop()
        {
            if (this.globalMouseListener != null)
            {
                this.globalMouseListener.Dispose();
            }

            this.isActived = false;
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