namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Components.Fonts;
    using Engine.Guis;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Synchronization;

    public class SynchronizationModuleVisual : ModuleVisual<SynchronizationModule, SynchronizationSettings, SynchronizationModuleLogic>
    {
        private string StateInfoFalse = "Losing Synchronicity";

        private string StateInfoTrue  = "Gaining Synchronicity";

        private readonly ICognition cognition;

        private readonly IConsciousness consciousness;

        private readonly ISynchronization synchronization;

        public SynchronizationModuleVisual(SynchronizationModule module, ICognition cognition, IConsciousness consciousness, ISynchronization synchronization)
            : base(module)
        {
            if (cognition == null)
            {
                throw new ArgumentNullException("cognition");
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            if (consciousness == null)
            {
                throw new ArgumentNullException("consciousness");
            }

            this.cognition = cognition;
            this.synchronization = synchronization;
            this.consciousness = consciousness;

            var progressBar = new SynchronizationProgressBar(this.Module, this.synchronization);

            var stateInfo = new Label(
                () => Font.UiStatistics,
                () => this.synchronization.Enabled ? this.StateInfoTrue : this.StateInfoFalse,
                () => this.StateInfoCenterPosition,
                () => Color.White,
                () => 1.1f,
                StringHAlign.Center,
                StringVAlign.Center);

            var statusInfo = new Label(
                () => Font.UiStatistics,
                () => string.Format("Level {0}: {1}", this.synchronization.Level, this.synchronization.State),
                () => this.StatusInfoCenterPosition,
                () => Color.White,
                () => 0.7f,
                StringHAlign.Center,
                StringVAlign.Center);

            var accumulationInfo = new Label(
                () => Font.UiStatistics,
                () => string.Format("{0}", this.synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                () => this.AccumulationInfoPosition,
                () => Color.White,
                () => 0.7f,
                StringHAlign.Right,
                StringVAlign.Center);

            var dailyRatePrefix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.consciousness.IsAwake
                    ? cognition.SynchronizationRatio.ToString("F0")
                    : "",
                () => this.DailyRateCenterPosition,
                () => Color.White,
                () => 2.0f,
                StringHAlign.Left,
                StringVAlign.Center);

            var dailyRateSubfix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.consciousness.IsAwake ? " %" : "",
                () => this.DailyRateCenterPosition,
                () => Color.White,
                () => 1f,
                StringHAlign.Right,
                StringVAlign.Center);

            var accelerationInfoPrefix = new Label(
                () => Font.UiStatistics,
                () => "x ",
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 1f,
                StringHAlign.Left,
                StringVAlign.Center);

            var accelerationInfoSubfix = new Label(
                () => Font.UiStatistics,
                () => string.Format("{0}", this.synchronization.Acceleration.ToString("F1")),
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 2.0f,
                StringHAlign.Right,
                StringVAlign.Center);

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
            for (var i = 0; i < this.synchronization.SynchronizedHourMax; ++i)
            {
                this.Entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Right));
            }

            // Yesterday points
            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Right));
            }

            // Today points
            for (var i = 0; i < this.synchronization.SynchronizedHourToday; ++i)
            {
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Right));
            }
        }

        #region Dependency

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
