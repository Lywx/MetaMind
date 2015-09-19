namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Component.Font;
    using Engine.Gui.Control.Visuals;
    using Engine.Service;
    using Microsoft.Xna.Framework;
    using Synchronization;

    public class SynchronizationModuleVisual : GameEntityModuleVisual<SynchronizationModule, SynchronizationSettings, SynchronizationModuleLogic>
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
                throw new ArgumentNullException(nameof(cognition));
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            if (consciousness == null)
            {
                throw new ArgumentNullException(nameof(consciousness));
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
                HoritonalAlignment.Center,
                VerticalAlignment.Center);

            var statusInfo = new Label(
                () => Font.UiStatistics,
                () => $"Level {this.synchronization.Level}: {this.synchronization.State}",
                () => this.StatusInfoCenterPosition,
                () => Color.White,
                () => 0.7f,
                HoritonalAlignment.Center,
                VerticalAlignment.Center);

            var accumulationInfo = new Label(
                () => Font.UiStatistics,
                () => $"{this.synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")}",
                () => this.AccumulationInfoPosition,
                () => Color.White,
                () => 0.7f,
                HoritonalAlignment.Right,
                VerticalAlignment.Center);

            var dailyRatePrefix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.consciousness.IsAwake
                    ? cognition.SynchronizationRate.ToString("F0")
                    : "",
                () => this.DailyRateCenterPosition,
                () => Color.White,
                () => 2.0f,
                HoritonalAlignment.Left,
                VerticalAlignment.Center);

            var dailyRateSubfix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.consciousness.IsAwake ? " %" : "",
                () => this.DailyRateCenterPosition,
                () => Color.White,
                () => 1f,
                HoritonalAlignment.Right,
                VerticalAlignment.Center);

            var accelerationInfoPrefix = new Label(
                () => Font.UiStatistics,
                () => "x ",
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 1f,
                HoritonalAlignment.Left,
                VerticalAlignment.Center);

            var accelerationInfoSubfix = new Label(
                () => Font.UiStatistics,
                () => $"{this.synchronization.Acceleration.ToString("F1")}",
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 2.0f,
                HoritonalAlignment.Right,
                VerticalAlignment.Center);

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

        private Vector2 AccelerationInfoCenterPosition => this.StatusInfoCenterPosition + new Vector2(160, 0);

        private Vector2 AccumulationInfoPosition => this.StateInfoCenterPosition + new Vector2(170, 0);

        private Vector2 DailyRateCenterPosition => this.StatusInfoCenterPosition + new Vector2(-160, 0);

        private Vector2 StateInfoCenterPosition => this.Settings.BarFrameCenterPosition + new Vector2(0, 1);

        private Vector2 StatusInfoCenterPosition => this.Settings.BarFrameCenterPosition + new Vector2(0, 34);

        #endregion

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Entities.Draw(graphics, time, alpha);
        }
    }
}
