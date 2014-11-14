using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;

namespace MetaMind.Perseverance.Guis.Widgets.Synchronizations
{
    /// <summary>
    /// An attention monitor during synchronization
    /// </summary>
    public class SynchronizationHudMonitor : GameComponent
    {
        private readonly TimeSpan attentionSpan            = TimeSpan.FromSeconds(5);

        private          DateTime attentionTriggeredMoment = DateTime.Now;
        private          DateTime musicPlayedMoment        = DateTime.Now;

        private MouseHookListener globalMouseListener;

        private bool isActived;

        #region Constructors

        public SynchronizationHudMonitor(Game game)
            : base(game)
        {
        }

        #endregion Constructors

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
            Game.Components.Add(this);
        }

        public void Stop()
        {
            if (globalMouseListener != null)
            {
                globalMouseListener.Dispose();
            }

            isActived = false;
            Game.Components.Remove(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!isActived)
            {
                return;
            }

            if (DateTime.Now - attentionTriggeredMoment > attentionSpan)
            {
                AudioManager.PlayMusic("Windows Proximity Notification");
                attentionTriggeredMoment = DateTime.Now;
            }
        }

        private void TriggerAttention(object sender, MouseEventExtArgs e)
        {
            attentionTriggeredMoment = DateTime.Now;
        }
    }
}