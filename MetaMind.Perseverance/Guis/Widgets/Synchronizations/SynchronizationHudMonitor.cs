namespace MetaMind.Perseverance.Guis.Widgets.Synchronizations
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

            Game.Components.Add(this);
        }

        public void TryStart()
        {
            if (!isActived)
            {
                this.Start();
            }
        }

        public void Start()
        {
            // Note: for an application hook, use the AppHooker class instead
            globalMouseListener = new MouseHookListener(new GlobalHooker()) { Enabled = true };

            // Set the event handler
            // recommended to use the Extended handlers, which allow input suppression among other additional information
            globalMouseListener.MouseMoveExt += TriggerAttention;

            isActived = true;
        }

        public void Stop()
        {
            if (globalMouseListener != null)
            {
                globalMouseListener.Dispose();
            }

            isActived = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (synchronization.Enabled && 
                DateTime.Now - attentionTriggeredMoment > attentionSpan)
            {
                GameEngine.AudioManager.PlayMusic("Windows Proximity Notification");

                attentionTriggeredMoment = DateTime.Now;
            }
            else if (!synchronization.Enabled)
            {
                GameEngine.AudioManager.PlayMusic("Windows Proximity Connection");

                attentionTriggeredMoment = DateTime.Now;
            }
        }

        private void TriggerAttention(object sender, MouseEventExtArgs e)
        {
            attentionTriggeredMoment = DateTime.Now;
        }
    }
}