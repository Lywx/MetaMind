namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SynchronizationModule : Module<SynchronizationModuleSettings>
    {
        public const string SyncFalseInfo = "Losing Synchronicity";
        public const string SyncTrueInfo  = "Synchronizing";

        private readonly ICognition       cognition;
        private readonly ISynchronization synchronization;

        private readonly SynchronizationMonitor monitor;

        private readonly SynchronizationValve valve;

        private SynchronizationModuleSleepStartedEventListener    sleepStartedEventListener;

        private SynchronizationModuleSynchronizationStartListener synchronizationStartListener;

        private SynchronizationModuleSynchronizationStopListener  synchronizationStopListener;

        #region Constructors

        public SynchronizationModule(Cognition cognition, SynchronizationModuleSettings settings)
            : base(settings)
        {
            this.cognition       = cognition;
            this.synchronization = cognition.Synchronization;

            // best close the mouse listener
            // which may casue severe mouse performance issues
            this.monitor = new SynchronizationMonitor(ScreenManager.Game, synchronization, false);
            this.valve   = new SynchronizationValve();
        }

        #endregion Constructors

        #region Graphical Position

        private Vector2 AccelerationPrefixCenter
        {
            get
            {
                return new Vector2(
                    this.StatusInfoCenter.X + this.Settings.AccelerationMargin.X,
                    this.StatusInfoCenter.Y + this.Settings.AccelerationMargin.Y);
            }
        }

        private Vector2 AccelerationSubfixCenter
        {
            get
            {
                const int XSymbolWidth = 43;
                return new Vector2(
                    this.AccelerationPrefixCenter.X + XSymbolWidth,
                    this.AccelerationPrefixCenter.Y);
            }
        }

        private Vector2 AccumulationPrefixCenter
        {
            get
            {
                return new Vector2(
                    this.StateInfoCenter.X + this.Settings.AccumulationMargin.X,
                    this.StateInfoCenter.Y + this.Settings.AccumulationMargin.Y);
            }
        }

        private Vector2 AccumulationSubfixCenter
        {
            get
            {
                const int PlusSymbolWidth = 42;
                return new Vector2(
                    this.AccumulationPrefixCenter.X + PlusSymbolWidth,
                    this.AccumulationPrefixCenter.Y);
            }
        }

        private Vector2 DailySyncHourPrefixCenter
        {
            get
            {
                return new Vector2(
                    this.StatusInfoCenter.X - this.Settings.SynchronizationRateMargin.X,
                    this.StatusInfoCenter.Y + this.Settings.SynchronizationRateMargin.Y);
            }
        }

        private Vector2 MessageCenter
        {
            get
            {
                return new Vector2((int)this.StateInfoCenter.X, GraphicsSettings.Height - 15);
            }
        }

        private Rectangle ProgressBarRectangle
        {
            get
            {
                return new Rectangle(
                    this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                    this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                    (int)(this.synchronization.ProgressPercent * this.Settings.BarFrameSize.X),
                    this.Settings.BarFrameSize.Y);
            }
        }

        private Rectangle ProgressFrameRectangle
        {
            get
            {
                return new Rectangle(
                    this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                    this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                    this.Settings.BarFrameSize.X,
                    this.Settings.BarFrameSize.Y);
            }
        }

        private Vector2 StateInfoCenter
        {
            get
            {
                return new Vector2(
                    this.Settings.BarFrameXC,
                    this.Settings.BarFrameYC + this.Settings.StateMargin.Y);
            }
        }

        private Vector2 StatusInfoCenter
        {
            get
            {
                return new Vector2(
                    this.Settings.BarFrameXC,
                    this.Settings.BarFrameYC + this.Settings.StatusMargin.Y);
            }
        }

        private Rectangle ValveFrameRectangle
        {
            get
            {
                return new Rectangle(
                    this.Settings.ValveFrameX - this.Settings.ValveFrameSize.X / 2,
                    this.Settings.ValveFrameY - this.Settings.ValveFrameSize.Y / 2,
                    this.Settings.ValveFrameSize.X,
                    this.Settings.ValveFrameSize.Y);
            }
        }

        private Vector2 ValveStateInfoPrefixCenter
        {
            get
            {
                return new Vector2(GraphicsSettings.Width / 2f, this.StatusInfoCenter.Y + 30);
            }
        }

        private Vector2 ValveStateInfoSubfixCenter
        {
            get
            {
                return this.ValveStateInfoPrefixCenter + new Vector2(0, 83);
            }
        }

        #endregion Locations

        #region Load and Unload

        public override void Load()
        {
            if (this.synchronizationStartListener == null || 
                this.synchronizationStopListener  == null || 
                this.sleepStartedEventListener    == null)
            {
                this.synchronizationStartListener = new SynchronizationModuleSynchronizationStartListener(this.synchronization, this);
                this.synchronizationStopListener  = new SynchronizationModuleSynchronizationStopListener(this.synchronization, this);
                this.sleepStartedEventListener    = new SynchronizationModuleSleepStartedEventListener(this.synchronization, this);
            }

            EventManager.AddListener(this.synchronizationStartListener);
            EventManager.AddListener(this.synchronizationStopListener);
            EventManager.AddListener(this.sleepStartedEventListener);
        }

        public override void Unload()
        {
            if (this.synchronizationStartListener != null)
            {
                EventManager.RemoveListener(this.synchronizationStartListener);
            }

            if (this.synchronizationStopListener != null)
            {
                EventManager.RemoveListener(this.synchronizationStopListener);
            }

            if (this.sleepStartedEventListener != null)
            {
                EventManager.RemoveListener(this.sleepStartedEventListener);
            }

            this.synchronizationStartListener = null;
            this.synchronizationStopListener  = null;
            this.sleepStartedEventListener    = null;
        }

        #endregion

        #region Operations

        public void StartSynchronizing(TaskEntry target)
        {
            if (valve.Opened)
            {
                this.synchronization.TryStart(target);
            }
        }

        public void StopSynchronizing()
        {
            if (valve.Opened)
            {
                this.synchronization.Stop();
                this.monitor        .Stop();
            }
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
            if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ForceReset))
            {
                this.synchronization.ResetForTomorrow();
            }

            if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ForceReverse))
            {
                this.synchronization.TryAbort();
            }

            if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ForceFlip))
            {
                this.valve.Flip();
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.monitor.TryStart();
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.DrawProgressFrame();
            this.DrawDotFrame();

            this.DrawStateInfo();
            this.DrawStatusInfo();

            this.DrawValveStatusInfo();

            this.DrawAccelerationInfo();
            this.DrawAccumulationInfo();
            
            this.DrawDailyRateInfo();

            this.DrawMassage(gameTime);

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);

            this.DrawProgressBar();
            this.DrawDot();

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin();
        }

        private void DrawAccelerationInfo()
        {
            FontManager.DrawCenteredText(
                this.Settings.AccelerationFont,
                "x",
                this.AccelerationPrefixCenter,
                this.Settings.AccelerationColor,
                1f);
            FontManager.DrawCenteredText(
                this.Settings.AccelerationFont,
                string.Format("{0}", this.synchronization.Acceleration.ToString("F1")),
                this.AccelerationSubfixCenter,
                this.Settings.AccelerationColor,
                this.Settings.AccelerationSize);
        }

        private void DrawAccumulationInfo()
        {
            FontManager.DrawCenteredText(
                this.Settings.AccumulationFont,
                string.Format("{0}", this.synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                this.AccumulationSubfixCenter,
                this.Settings.AccumulationColor,
                this.Settings.AccumulationSize);
        }

        private void DrawDailyRateInfo()
        {
            var awake = this.cognition.Consciousness as ConsciousnessAwake;
            if (awake != null)
            {
                var awakeTime = awake.AwakeSpan;

                var synchronizedTime = this.synchronization.SynchronizedTimeToday
                                       + (this.synchronization.Enabled
                                              ? this.synchronization.ElapsedTimeSinceTransition
                                              : TimeSpan.Zero);

                var syncRate = synchronizedTime.TotalSeconds / awakeTime.TotalSeconds;
                var syncRateText = (syncRate * 100).ToString("F0");

                // draw rate digits
                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationRateFont,
                    syncRateText,
                    this.DailySyncHourPrefixCenter,
                    this.Settings.SynchronizationRateColor,
                    this.Settings.SynchronizationRateSize);

                const int    SymbolMargin = 10;
                const string Symbol = "%";

                var syncRateTextWidth = this.Settings.SynchronizationRateFont.MeasureString(syncRateText).X;
                var syncRateTextMargin = new Vector2(syncRateTextWidth / 2 * this.Settings.SynchronizationRateSize + SymbolMargin, 0);

                // draw % after rate digits
                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationRateFont,
                    Symbol,
                    this.DailySyncHourPrefixCenter + syncRateTextMargin,
                    this.Settings.SynchronizationRateColor,
                    1f);
            }
        }

        private void DrawDot()
        {
            // left side content
            for (var i = 0; i < this.synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    this.SynchronizationDotRectangle(i, true),
                    this.Settings.BarFrameAscendColor);
            }

            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    this.SynchronizationDotRectangle(i, true),
                    this.Settings.BarFrameDescendColor);
            }

            // right side content
            for (var i = 0; i < this.synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    this.SynchronizationDotRectangle(i, false),
                    this.Settings.BarFrameAscendColor);
            }

            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    this.SynchronizationDotRectangle(i, false),
                    this.Settings.BarFrameDescendColor);
            }
        }

        private void DrawDotFrame()
        {
            // left side frame
            for (var i = 0; i < this.synchronization.SynchronizedHourMax; ++i)
            {
                var dotFrame = this.SynchronizationDotRectangle(i, true);

                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    dotFrame,
                    this.Settings.SynchronizationDotFrameColor);

                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationRateFont,
                    ((i + 1) % 10).ToString("F0"),
                    this.SynchronizationDotTextCenter(dotFrame),
                    this.Settings.SynchronizationRateColor,
                    0.7f);
            }

            // right side frame
            for (var i = 0; i < this.synchronization.SynchronizedHourMax; ++i)
            {
                var dotFrame = this.SynchronizationDotRectangle(i, false);

                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    dotFrame,
                    this.Settings.SynchronizationDotFrameColor);

                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationRateFont,
                    ((i + 1) % 10).ToString("F0"),
                    this.SynchronizationDotTextCenter(dotFrame),
                    this.Settings.SynchronizationRateColor,
                    0.7f);
            }
        }

        private void DrawMassage(GameTime gameTime)
        {
            var alpha  = (byte)(255 * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3)));
            var better = this.synchronization.SynchronizedHourToday >= this.synchronization.SynchronizedHourYesterday;

            const string IncreaseMessage = "Computational Motivation Synchronization Ratio increases";
            const string DecreaseMessage = "Computational Motivation Synchronization Ratio decreases";

            FontManager.DrawCenteredText(
                this.Settings.MessageFont,
                better ? IncreaseMessage : DecreaseMessage,
                this.MessageCenter,
                (better ? this.Settings.BarFrameAscendColor : this.Settings.BarFrameDescendColor).MakeTransparent(alpha),
                this.Settings.MessageSize);
        }

        private void DrawProgressBar()
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.ProgressBarRectangle,
                this.synchronization.Enabled ? this.Settings.BarFrameAscendColor : this.Settings.BarFrameDescendColor);
        }

        private void DrawProgressFrame()
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, this.ProgressFrameRectangle, this.Settings.BarFrameBackgroundColor);
        }

        private void DrawStateInfo()
        {
            FontManager.DrawCenteredText(
                this.Settings.StateFont,
                this.synchronization.Enabled ? SyncTrueInfo : SyncFalseInfo,
                this.StateInfoCenter,
                this.Settings.StateColor,
                this.Settings.StateSize);
        }

        private void DrawStatusInfo()
        {
            FontManager.DrawCenteredText(
                this.Settings.StateFont,
                string.Format("Level {0}: {1}", this.synchronization.Level, this.synchronization.State),
                this.StatusInfoCenter,
                this.Settings.StatusColor,
                this.Settings.StatusSize);
        }

        private void DrawValveStatusInfo()
        {
            const string PrefixText = "Approximated Motive";
            FontManager.DrawCenteredText(
                this.Settings.ValveStateFont,
                PrefixText,
                this.ValveStateInfoPrefixCenter,
                this.valve.Opened ? this.Settings.ValueAscendColor : this.Settings.ValueDescendColor,
                0.7f);

            FontManager.DrawCenteredText(
                this.Settings.ValveStateFont,
                "[   ]",
                this.ValveStateInfoSubfixCenter,
                this.valve.Opened ? this.Settings.ValueAscendColor : this.Settings.ValueDescendColor,
                this.Settings.ValueStatusSize);
        }

        private Rectangle SynchronizationDotRectangle(int i, bool leftsided)
        {
            return RectangleExt.Rectangle(
                leftsided ? (int)this.StateInfoCenter.X - 275 - 15 * i : (int)this.StateInfoCenter.X + 275 + 15 * i,
                (int)this.StateInfoCenter.Y - 1,
                this.Settings.BarFrameSize.Y,
                this.Settings.BarFrameSize.Y);
        }

        private Vector2 SynchronizationDotTextCenter(Rectangle dotFrame)
        {
            return dotFrame.Center.ToVector2() + new Vector2(0, this.Settings.BarFrameSize.Y * 2);
        }

        #endregion Update and Draw
    }
}