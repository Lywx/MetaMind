namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Extensions;
    using MetaMind.Perseverance.Guis.Modules.Summary;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    namespace Summary
    {
        public class SleepStoppedListener : Listener
        {
            public SleepStoppedListener()
            {
                this.RegisteredEvents.Add((int)SessionEventType.SleepStopped);
            }

            public override bool HandleEvent(IEvent @event)
            {
                var screenManager = this.GameInterop.Screen;

                var summary = screenManager.Screens.First(screen => screen is SummaryScreen);
                if (summary != null)
                {
                    summary.Exit();
                }

                screenManager.AddScreen(new MotivationScreen());

                return true;
            }
        }
    }

    public class SummaryModule : Module<SummarySettings>
    {
        private ICognition       Cognition;
        private ISynchronization Synchronization;

        private List<GameVisualEntity> entities;
        #region Constructors

        public SummaryModule(ICognition cognition, SummarySettings settings)
            : base(settings)
        {
            this.Cognition       = cognition;
            this.Synchronization = cognition.Synchronization;

            this.entities = new List<GameVisualEntity>();
        }

        #endregion Constructors

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            var factory = new SummaryFactory(this.Settings);

            this.entities.Add(
                new SummaryTitle(
                    () => this.Settings.TitleFont,
                    () => "Summary",
                    () => this.Settings.TitleCenter,
                    () => this.Settings.TitleColor,
                    () => this.Settings.TitleSize));

            // Daily statistics
            this.entities.Add(
                factory.CreateEntry(
                    1,
                    "Hours in Synchronization:",
                    () => this.Synchronization.SynchronizedHourYesterday.ToSummary(),
                    Color.White));

            this.entities.Add(
                factory.CreateEntry(
                    3,
                    "Hours in Good Profession:",
                    () => (-this.Settings.HourOfGood).ToSummary(),
                    Color.Red));
            this.entities.Add(
                factory.CreateEntry(
                    4,
                    "Hours in Lofty Profession:",
                    () => (-this.Settings.HourOfLofty).ToSummary(),
                    Color.Red));

            this.entities.Add(factory.CreateSplit(5, Color.Red));

            this.entities.Add(
                factory.CreateEntry(
                    () => 6,
                    () => "",
                    () => (this.Synchronization.SynchronizedHourYesterday - this.Settings.HourOfGood - this.Settings.HourOfLofty).ToSummary(),
                    () => (this.Synchronization.SynchronizedHourYesterday - this.Settings.HourOfGood - this.Settings.HourOfLofty >= 0 ? Color.White : Color.Red)));

            // Weekly statistics
            this.entities.Add(
                factory.CreateEntry(
                    8,
                    "Hours in Synchronization In Recent 7 Days:",
                    () => ((int)this.Synchronization.SynchronizedTimeRecentWeek.TotalHours).ToSummary(),
                    Color.White));

            this.entities.Add(
                factory.CreateEntry(
                    10,
                    "Len Bosack's Records in 7 Days:",
                    () => (-this.Settings.HourOfWorldRecord).ToSummary(),
                    Color.Red));

            this.entities.Add(factory.CreateSplit(11, Color.Red));

            this.entities.Add(
                factory.CreateEntry(
                    () => 12,
                    () => "",
                    () => ((int)this.Synchronization.SynchronizedTimeRecentWeek.TotalHours - this.Settings.HourOfWorldRecord).ToSummary(),
                    () => ((int)this.Synchronization.SynchronizedTimeRecentWeek.TotalHours - this.Settings.HourOfWorldRecord >= 0
                         ? Color.Gold
                         : Color.Red)));

            this.Listeners.Add(new SleepStoppedListener());

            base.LoadContent(interop);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.Awaken))
            {
                this.Cognition.Consciousness.Awaken();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.Sleep))
            {
                this.Cognition.Consciousness.Sleep();
            }
        }
        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this.entities)
            {
                entity.Draw(graphics, time, alpha);
            }
        }

        #endregion 
    }
}