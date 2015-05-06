namespace MetaMind.Testimony.Guis.Modules
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Testimony.Concepts.Synchronizations;
    using MetaMind.Testimony.Events;
    using MetaMind.Testimony.Sessions;

    using Microsoft.Xna.Framework;

    public class ExperienceModule : Module<ExperienceSettings>
    {
        public ExperienceModule(ExperienceSettings settings)
            : base(settings)
        {
            this.SynchronizationData = new SynchronizationData();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Enter))
            {
                this.SwitchSync();
            }
        }

        #region Pure Synchronization

        public void StartSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(
                new Event(
                    (int)SessionEventType.SyncStarted,
                    new SynchronizationStartedEventArgs(this.SynchronizationData)));
        }

        public void StopSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(
                new Event(
                    (int)SessionEventType.SyncStopped,
                    new SynchronizationStoppedEventArgs(this.SynchronizationData)));
        }

        public void SwitchSync()
        {
            if (!this.SynchronizationData.IsSynchronizing)
            {
                this.StartSync();
            }
            else
            {
                this.StopSync();
            }
        }

        public ISynchronizationData SynchronizationData { get; set; }

        #endregion
    }
}
