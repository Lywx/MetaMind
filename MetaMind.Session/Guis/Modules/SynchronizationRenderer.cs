namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Engine.Components.Content.Fonts;
    using Engine.Entities;
    using Engine.Entities.Bases;
    using Engine.Entities.Controls.Labels;
    using Engine.Entities.Graphics.Fonts;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Runtime;
    using Runtime.Attention;
    using Synchronization;

    public class SynchronizationRenderer : MMMVCEntityRenderer<SynchronizationModule, SynchronizationSettings, SynchronizationController>
    {
        private string StateInfoFalse = "Losing Synchronicity";

        private string StateInfoTrue  = "Gaining Synchronicity";

        private readonly ICognition cognition;

        private readonly IConsciousness consciousness;

        private readonly ISynchronizationData synchronizationData;

        public SynchronizationRenderer(SynchronizationModule module, ICognition cognition, IConsciousness consciousness, ISynchronizationData synchronizationData)
            : base(module)
        {
            if (cognition == null)
            {
                throw new ArgumentNullException(nameof(cognition));
            }

            if (synchronizationData == null)
            {
                throw new ArgumentNullException(nameof(synchronizationData));
            }

            if (consciousness == null)
            {
                throw new ArgumentNullException(nameof(consciousness));
            }

            this.cognition = cognition;
            this.synchronizationData = synchronizationData;
            this.consciousness = consciousness;

            var progressBar = new SynchronizationProgressBar(this.Module, this.synchronizationData);

            var stateInfo = new Label(
                () => MMFont.UiStatistics,
                () => this.synchronizationData.Enabled ? this.StateInfoTrue : this.StateInfoFalse,
                () => this.StateInfoCenterPosition,
                () => Color.White,
                () => 1.1f,
                HoritonalAlignment.Center,
                VerticalAlignment.Center);

            var statusInfo = new Label(
                () => MMFont.UiStatistics,
                () => $"Level {this.synchronizationData.Level}: {this.synchronizationData.State}",
                () => this.StatusInfoCenterPosition,
                () => Color.White,
                () => 0.7f,
                HoritonalAlignment.Center,
                VerticalAlignment.Center);

            var accumulationInfo = new Label(
                () => MMFont.UiStatistics,
                () => $"{this.synchronizationData.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")}",
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
                () => MMFont.UiStatistics,
                () => "x ",
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 1f,
                HoritonalAlignment.Left,
                VerticalAlignment.Center);

            var accelerationInfoSubfix = new Label(
                () => MMFont.UiStatistics,
                () => $"{this.synchronizationData.Acceleration.ToString("F1")}",
                () => this.AccelerationInfoCenterPosition,
                () => Color.White,
                () => 2.0f,
                HoritonalAlignment.Right,
                VerticalAlignment.Center);

            this.Entities = new MMEntityCollection<IMMVisualEntity>
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
            for (var i = 0; i < this.synchronizationData.SynchronizedHourMax; ++i)
            {
                this.Entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Right));
            }

            // Yesterday points
            for (var i = 0; i < this.synchronizationData.SynchronizedHourYesterday; ++i)
            {
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Right));
            }

            // Today points
            for (var i = 0; i < this.synchronizationData.SynchronizedHourToday; ++i)
            {
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Left));
                this.Entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Right));
            }
        }

        #region Dependency

        private MMEntityCollection<IMMVisualEntity> Entities { get; set; }

        #endregion

        #region Positional Data

        private Vector2 AccelerationInfoCenterPosition => this.StatusInfoCenterPosition + new Vector2(160, 0);

        private Vector2 AccumulationInfoPosition => this.StateInfoCenterPosition + new Vector2(170, 0);

        private Vector2 DailyRateCenterPosition => this.StatusInfoCenterPosition + new Vector2(-160, 0);

        private Vector2 StateInfoCenterPosition => this.Settings.BarFrameCenterPosition + new Vector2(0, 1);

        private Vector2 StatusInfoCenterPosition => this.Settings.BarFrameCenterPosition + new Vector2(0, 34);

        #endregion

        public override void Draw(GameTime time)
        {
            this.Entities.Draw(time);
        }
    }
}
