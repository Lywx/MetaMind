namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Concepts.Cognitions;
    using MetaMind.Runtime.Concepts.Synchronizations;

    using Microsoft.Xna.Framework;

    public class SynchronizationModuleGraphics : ModuleGraphics<SynchronizationModule, SynchronizationSettings, SynchronizationModuleControl>
    {
        private string StateInfoFalse = "Losing Synchronicity";

        private string StateInfoTrue  = "Gaining Synchronicity";

        private List<GameVisualEntity> entities;

        public SynchronizationModuleGraphics(SynchronizationModule module, IConsciousness consciousness, ISynchronization synchronization)
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

            var dailySyncRatePrefix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.Consciousness.IsAwake
                    ? (this.Synchronization.PotentialSynchronizedTimeToday.TotalSeconds
                       / ((IConsciousnessAwake)this.Consciousness.State).AwakeSpan.TotalSeconds * 100).ToString("F0")
                    : "",
                () => this.DailySyncRateCenterPosition,
                () => Color.White,
                () => 2.0f,
                StringHAlign.Left,
                StringVAlign.Center,
                false);

            var dailySyncRateSubfix = new Label(
                () => this.Settings.SynchronizationRateFont,
                () => this.Consciousness.IsAwake ? " %" : "",
                () => this.DailySyncRateCenterPosition,
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

            this.entities = new List<GameVisualEntity>
                                {
                                    progressBar,

                                    stateInfo, // Gaining Synchronity
                                    statusInfo, // Level 0: unrecognizable

                                    accumulationInfo, // 00:00:14

                                    dailySyncRatePrefix, // 80
                                    dailySyncRateSubfix, // %

                                    accelerationInfoPrefix, // x
                                    accelerationInfoSubfix // 1.2
                                };

            var factory = new SynchronizationFactory(this.Settings);

            // Empty point frames
            for (var i = 0; i < this.Synchronization.SynchronizedHourMax; ++i)
            {
                this.entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Left));
                this.entities.Add(factory.CreatePointFrame(this.StateInfoCenterPosition, i, SynchronizationPointSide.Right));
            }

            // Yesterday points
            for (var i = 0; i < this.Synchronization.SynchronizedHourYesterday; ++i)
            {
                this.entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Left));
                this.entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameDescendColor, SynchronizationPointSide.Right));
            }

            // Today points
            for (var i = 0; i < this.Synchronization.SynchronizedHourToday; ++i)
            {
                this.entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Left));
                this.entities.Add(factory.CreatePoint(this.StateInfoCenterPosition, i, "", () => this.Settings.BarFrameAscendColor, SynchronizationPointSide.Right));
            }
        }

        #region Dependency

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

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

        private Vector2 DailySyncRateCenterPosition
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
            foreach (var entity in this.entities)
            {
                entity.Draw(graphics, time, alpha);
            }
        }
    }
}
