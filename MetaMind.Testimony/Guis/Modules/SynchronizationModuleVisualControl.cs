namespace MetaMind.Testimony.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Visuals;
    using MetaMind.Engine.Services;
    using MetaMind.Testimony.Concepts.Cognitions;
    using MetaMind.Testimony.Concepts.Synchronizations;
    using MetaMind.Testimony.Guis.Modules.Synchronization;

    using Microsoft.Xna.Framework;

    public class SynchronizationModuleVisualControl : ModuleVisualControl<SynchronizationModule, SynchronizationSettings, SynchronizationModuleLogicControl>
    {
        private string StateInfoFalse = "Losing Synchronicity";

        private string StateInfoTrue  = "Gaining Synchronicity";

        public SynchronizationModuleVisualControl(SynchronizationModule module, IConsciousness consciousness, ISynchronization synchronization)
            : base(module)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            if (consciousness == null)
            {
                throw new ArgumentNullException("consciousness");
            }

            this.Synchronization = synchronization;
            this.Consciousness = consciousness;

            var progressBar = new SynchronizationProgressBar(this.Module, this.Synchronization);

            var stateInfo = new Label(
                () => Font.UiStatistics,
                () => this.Synchronization.Enabled ? this.StateInfoTrue : this.StateInfoFalse,
                () => this.StateInfoCenterPosition,
                () => Color.White,
                () => 1.1f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            var statusInfo = new Label(
                () => Font.UiStatistics,
                () => string.Format("Level {0}: {1}", this.Synchronization.Level, this.Synchronization.State),
                () => this.StatusInfoCenterPosition,
                () => Color.White,
                () => 0.7f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            var accumulationInfo = new Label(
                () => Font.UiStatistics,
                () => string.Format("{0}", this.Synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                () => this.AccumulationInfoPosition,
                () => Color.White,
                () => 0.7f,
                StringHAlign.Right,
                StringVAlign.Center,
                false);

            var dailyRatePrefix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.Consciousness.IsAwake
                    ? (this.Synchronization.SynchronizedTimeTodayBestCase.TotalSeconds
                       / ((IConsciousnessAwake)this.Consciousness.State).AwakeSpan.TotalSeconds * 100).ToString("F0")
                    : "",
                () => this.DailyRateCenterPosition,
                () => Color.White,
                () => 2.0f,
                StringHAlign.Left,
                StringVAlign.Center,
                false);

            var dailyRateSubfix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.Consciousness.IsAwake ? " %" : "",
                () => this.DailyRateCenterPosition,
                () => Color.White,
                () => 1f,
                StringHAlign.Right,
                StringVAlign.Center,
                false);

            var accelerationInfoPrefix = new Label(
                () => Font.UiStatistics,
                () => "x ",
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 1f,
                StringHAlign.Left,
                StringVAlign.Center,
                false);

            var accelerationInfoSubfix = new Label(
                () => Font.UiStatistics,
                () => string.Format("{0}", this.Synchronization.Acceleration.ToString("F1")),
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 2.0f,
                StringHAlign.Right,
                StringVAlign.Center,
                false);

            this.Entities = new GameVisualEntityCollection<IGameVisualEntity>
                                {
                                    progressBar,

                                    stateInfo, // Gaining Synchronity
                                    statusInfo, // Level 0: unrecognizable

                                    accumulationInfo, // 00:00:14

                                    dailyRatePrefix, // 80
                                    dailyRateSubfix, // %

                                    accelerationInfoPrefix, // x
                                    accelerationInfoSubfix // 1.2
                                };

            var factory = new SynchronizationFactory(this.Settings);

            // Empty point frames
            for (var i = 0; i < this.Synchronization.SynchronizedHourMax; ++i)
            {
                this.Entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Right));
            }

            // Yesterday points
            for (var i = 0; i < this.Synchronization.SynchronizedHourYesterday; ++i)
            {
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Right));
            }

            // Today points
            for (var i = 0; i < this.Synchronization.SynchronizedHourToday; ++i)
            {
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Right));
            }
        }

        #region Dependency

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

        private GameVisualEntityCollection<IGameVisualEntity> Entities { get; set; }

        #endregion

        #region Positional Data

        private Vector2 AccelerationInfoCenterPosition
        {
            get
            {
                return this.StatusInfoCenterPosition + new Vector2(160, 0);
            }
        }

        private Vector2 AccumulationInfoPosition
        {
            get
            {
                return this.StateInfoCenterPosition + new Vector2(170, 0);
            }
        }

        private Vector2 DailyRateCenterPosition
        {
            get
            {
                return this.StatusInfoCenterPosition + new Vector2(-160, 0);
            }
        }

        private Vector2 StateInfoCenterPosition
        {
            get
            {
                return this.Settings.BarFrameCenterPosition + new Vector2(0, 1);
            }
        }

        private Vector2 StatusInfoCenterPosition
        {
            get
            {
                return this.Settings.BarFrameCenterPosition + new Vector2(0, 34);
            }
        }

        #endregion

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Entities.Draw(graphics, time, alpha);
        }
    }
}
