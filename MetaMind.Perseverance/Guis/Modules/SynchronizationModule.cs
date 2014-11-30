namespace MetaMind.Perseverance.Guis.Modules
{
    using System;
    using System.Runtime.Remoting.Messaging;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SynchronizationModule : Module<SynchronizationHudSettings>
    {
        private ISynchronization                               synchronization;
        private IConsciousness                                 consciousness;

        private SynchronizationHudMonitor                      monitor;

        private SynchronizationHudSynchronizationStartListener synchronizationStartListener;
        private SynchronizationHudSynchronizationStopListener  synchronizationStopListener;

        private SynchronizationHudSleepStartedEventListener    sleepStartedEventListener;

        #region Constructors

        public SynchronizationModule(ISynchronization synchronization, IConsciousness consciousness, SynchronizationHudSettings settings)
            : base(settings)
        {
            this.synchronization = synchronization;
            this.consciousness   = consciousness;

            this.monitor = new SynchronizationHudMonitor(ScreenManager.Game, synchronization);
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

        private Vector2 DailySyncHourSubfixCenter
        {
            get
            {
                const int PercentNumWidth = 50;
                return new Vector2(
                    this.DailySyncHourPrefixCenter.X + PercentNumWidth,
                    this.DailySyncHourPrefixCenter.Y);
            }
        }

        private Vector2 MessageCenter
        {
            get
            {
                return new Vector2(
                    (int)this.StateInfoCenter.X,
                    GraphicsSettings.Height - 15);
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
                    this.Settings.BarFrameYC + this.Settings.InformationMargin.Y);
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
                this.synchronizationStartListener = new SynchronizationHudSynchronizationStartListener(this.synchronization, this);
                this.synchronizationStopListener  = new SynchronizationHudSynchronizationStopListener(this.synchronization, this);
                this.sleepStartedEventListener    = new SynchronizationHudSleepStartedEventListener(this.synchronization, this);
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
            this.synchronization.TryStart(target);
        }

        public void StopSynchronizing()
        {
            this.synchronization.Stop();
            this.monitor        .Stop();
        }

        #endregion Operations

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.DrawProgressFrame();
            this.DrawSynchronizationDotFrame();

            this.DrawStateInformation();
            this.DrawStatusInformation();

            this.DrawAccelerationIndicator();

            this.DrawAccumulationIndicator();
            
            this.DrawDailySyncRateIndicator();

            this.DrawMassage(gameTime);

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);

            this.DrawProgressBar();
            this.DrawSynchronizationDot();

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin();
        }

        public override void UpdateInput(GameTime gameTime)
        {
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.monitor.TryStart();
        }

        private void DrawAccelerationIndicator()
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

        private void DrawAccumulationIndicator()
        {
            FontManager.DrawCenteredText(
                this.Settings.AccumulationFont,
                string.Format("{0}", this.synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                this.AccumulationSubfixCenter,
                this.Settings.AccumulationColor,
                this.Settings.AccumulationSize);
        }

        private void DrawDailySyncRateIndicator()
        {
            var awake = this.consciousness as ConsciousnessAwake;
            if (awake != null)
            {
                var awakeTime = awake.AwakeSpan;

                var synchronizedTime = this.synchronization.SynchronizedTimeToday
                                       + (this.synchronization.Enabled
                                              ? this.synchronization.ElapsedTimeSinceTransition
                                              : TimeSpan.Zero);

                var syncRate = synchronizedTime.TotalSeconds / awakeTime.TotalSeconds;

                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationRateFont,
                    (syncRate * 100).ToString("F0"),
                    this.DailySyncHourPrefixCenter,
                    this.Settings.SynchronizationRateColor,
                    this.Settings.SynchronizationRateSize);

                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationRateFont,
                    "%",
                    this.DailySyncHourSubfixCenter,
                    this.Settings.SynchronizationRateColor,
                    1f);
            }
        }

        private void DrawMassage(GameTime gameTime)
        {
            var alpha  = (byte)(255 * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3)));
            var better = this.synchronization.SynchronizedHourToday >= this.synchronization.SynchronizedHourYesterday;

            const string HappyNotice   = "Look like you are gonna be more happier from today.";
            const string UnhappyNotice = "Look like you are gonna be less happier from today.";

            FontManager.DrawCenteredText(
                this.Settings.MessageFont,
                better ? HappyNotice : UnhappyNotice,
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

        private void DrawStateInformation()
        {
            const string SyncTrueInfo  = "Synchronizing";
            const string SyncFalseInfo = "Losing Synchronicity";
            FontManager.DrawCenteredText(
                this.Settings.StateFont,
                this.synchronization.Enabled ? SyncTrueInfo : SyncFalseInfo,
                this.StateInfoCenter,
                this.Settings.StateColor,
                this.Settings.StateSize);
        }

        private void DrawStatusInformation()
        {
            FontManager.DrawCenteredText(
                this.Settings.StateFont,
                string.Format("Level {0}: {1}", this.synchronization.Level, this.synchronization.State),
                this.StatusInfoCenter,
                this.Settings.StatusColor,
                this.Settings.StatusSize);
        }

        private void DrawSynchronizationDot()
        {
            // left side content
            for (var i = 0; i < this.synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X - 275 - 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.Settings.BarFrameSize.Y,
                        this.Settings.BarFrameSize.Y),
                    this.Settings.BarFrameAscendColor);
            }

            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X - 275 - 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.Settings.BarFrameSize.Y,
                        this.Settings.BarFrameSize.Y),
                    this.Settings.BarFrameDescendColor);
            }

            // right side content
            for (var i = 0; i < this.synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X + 275 + 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.Settings.BarFrameSize.Y,
                        this.Settings.BarFrameSize.Y),
                    this.Settings.BarFrameAscendColor);
            }

            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X + 275 + 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.Settings.BarFrameSize.Y,
                        this.Settings.BarFrameSize.Y),
                    this.Settings.BarFrameDescendColor);
            }
        }

        private void DrawSynchronizationDotFrame()
        {
            // left side frame
            for (var i = 0; i < this.synchronization.SynchronizedHourMax; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X - 275 - 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.Settings.BarFrameSize.Y,
                        this.Settings.BarFrameSize.Y),
                    this.Settings.SynchronizationDotFrameColor);
            }

            // right side frame
            for (var i = 0; i < this.synchronization.SynchronizedHourMax; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X + 275 + 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.Settings.BarFrameSize.Y,
                        this.Settings.BarFrameSize.Y),
                    this.Settings.SynchronizationDotFrameColor);
            }
        }

        #endregion Update and Draw
    }
}