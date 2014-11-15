namespace MetaMind.Perseverance.Guis.Widgets.Synchronizations
{
    using System;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SynchronizationHud : Widget
    {
        private IConsciousness                                 consciousness;
        private SynchronizationHudMonitor                      monitor;
        private SynchronizationHudSettings                     settings;
        private ISynchronization                               synchronization;
        private SynchronizationHudSynchronizationStartListener synchronizationStartListener;
        private SynchronizationHudSynchronizationStopListener  synchronizationStopListener;

        #region Constructors

        public SynchronizationHud(ISynchronization synchronization, IConsciousness consciousness, SynchronizationHudSettings settings)
        {
            this.synchronization = synchronization;
            this.consciousness   = consciousness;
            this.settings        = settings;

            this.monitor = new SynchronizationHudMonitor(ScreenManager.Game, synchronization);

            synchronizationStartListener = new SynchronizationHudSynchronizationStartListener(synchronization, this);
            synchronizationStopListener  = new SynchronizationHudSynchronizationStopListener(synchronization, this);

            EventManager.AddListener(synchronizationStartListener);
            EventManager.AddListener(synchronizationStopListener);
        }

        #endregion Constructors

        #region Locations

        private Vector2 AccelerationPrefixCenter
        {
            get
            {
                return new Vector2(
                    this.StatusInfoCenter.X + this.settings.AccelerationMargin.X,
                    this.StatusInfoCenter.Y + this.settings.AccelerationMargin.Y);
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
                    this.StateInfoCenter.X + this.settings.AccumulationMargin.X,
                    this.StateInfoCenter.Y + this.settings.AccumulationMargin.Y);
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
                    this.StatusInfoCenter.X - this.settings.SynchronizationRateMargin.X,
                    this.StatusInfoCenter.Y + this.settings.SynchronizationRateMargin.Y);
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
                    this.settings.BarFrameXC - this.settings.BarFrameSize.X / 2,
                    this.settings.BarFrameYC - this.settings.BarFrameSize.Y / 2,
                    (int)(synchronization.ProgressPercent * this.settings.BarFrameSize.X),
                    this.settings.BarFrameSize.Y);
            }
        }

        private Rectangle ProgressFrameRectangle
        {
            get
            {
                return new Rectangle(
                    this.settings.BarFrameXC - this.settings.BarFrameSize.X / 2,
                    this.settings.BarFrameYC - this.settings.BarFrameSize.Y / 2,
                    this.settings.BarFrameSize.X,
                    this.settings.BarFrameSize.Y);
            }
        }

        private Vector2 StateInfoCenter
        {
            get
            {
                return new Vector2(
                    this.settings.BarFrameXC,
                    this.settings.BarFrameYC + this.settings.StateMargin.Y);
            }
        }

        private Vector2 StatusInfoCenter
        {
            get
            {
                return new Vector2(
                    this.settings.BarFrameXC,
                    this.settings.BarFrameYC + this.settings.InformationMargin.Y);
            }
        }

        #endregion Locations

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
            monitor.TryStart();
        }

        private void DrawAccelerationIndicator()
        {
            FontManager.DrawCenteredText(
                this.settings.AccelerationFont,
                "x",
                this.AccelerationPrefixCenter,
                this.settings.AccelerationColor,
                1f);
            FontManager.DrawCenteredText(
                this.settings.AccelerationFont,
                string.Format("{0}", synchronization.Acceleration.ToString("F1")),
                this.AccelerationSubfixCenter,
                this.settings.AccelerationColor,
                this.settings.AccelerationSize);
        }

        private void DrawAccumulationIndicator()
        {
            FontManager.DrawCenteredText(
                this.settings.AccumulationFont,
                string.Format("{0}", synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                this.AccumulationSubfixCenter,
                this.settings.AccumulationColor,
                this.settings.AccumulationSize);
        }

        private void DrawDailySyncRateIndicator()
        {
            var awake = (ConsciousnessAwake)this.consciousness;
            var awakeTime = awake.AwakeSpan;

            var synchronizedTime = synchronization.SynchronizedTimeToday + (synchronization.Enabled ? synchronization.ElapsedTimeSinceTransition : TimeSpan.Zero);
            var syncRate = synchronizedTime.TotalSeconds / awakeTime.TotalSeconds; 

            FontManager.DrawCenteredText(
                this.settings.SynchronizationRateFont,
                (syncRate * 100).ToString("F0"),
                this.DailySyncHourPrefixCenter,
                this.settings.SynchronizationRateColor,
                this.settings.SynchronizationRateSize);

            FontManager.DrawCenteredText(
                this.settings.SynchronizationRateFont,
                "%",
                this.DailySyncHourSubfixCenter,
                this.settings.SynchronizationRateColor,
                1f);
        }

        private void DrawMassage(GameTime gameTime)
        {
            var alpha  = (byte)(255 * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3)));
            var better = synchronization.SynchronizedHourToday >= synchronization.SynchronizedHourYesterday;

            const string HappyNotice   = "Look like you are gonna be more happier from today.";
            const string UnhappyNotice = "Look like you are gonna be less happier from today.";

            FontManager.DrawCenteredText(
                this.settings.MessageFont,
                better ? HappyNotice : UnhappyNotice,
                this.MessageCenter,
                (better ? this.settings.BarFrameAscendColor : this.settings.BarFrameDescendColor).MakeTransparent(alpha),
                this.settings.MessageSize);
        }

        private void DrawProgressBar()
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.ProgressBarRectangle,
                synchronization.Enabled ? this.settings.BarFrameAscendColor : this.settings.BarFrameDescendColor);
        }

        private void DrawProgressFrame()
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, this.ProgressFrameRectangle, this.settings.BarFrameBackgroundColor);
        }

        private void DrawStateInformation()
        {
            const string SyncTrueInfo  = "Synchronizing";
            const string SyncFalseInfo = "Losing Synchronicity";
            FontManager.DrawCenteredText(
                this.settings.StateFont,
                synchronization.Enabled ? SyncTrueInfo : SyncFalseInfo,
                this.StateInfoCenter,
                this.settings.StateColor,
                this.settings.StateSize);
        }

        private void DrawStatusInformation()
        {
            FontManager.DrawCenteredText(
                this.settings.StateFont,
                string.Format("Level {0}: {1}", synchronization.Level, synchronization.State),
                this.StatusInfoCenter,
                this.settings.StatusColor,
                this.settings.StatusSize);
        }

        private void DrawSynchronizationDot()
        {
            // left side content
            for (var i = 0; i < synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X - 275 - 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.settings.BarFrameSize.Y,
                        this.settings.BarFrameSize.Y),
                    this.settings.BarFrameAscendColor);
            }

            for (var i = 0; i < synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X - 275 - 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.settings.BarFrameSize.Y,
                        this.settings.BarFrameSize.Y),
                    this.settings.BarFrameDescendColor);
            }

            // right side content
            for (var i = 0; i < synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X + 275 + 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.settings.BarFrameSize.Y,
                        this.settings.BarFrameSize.Y),
                    this.settings.BarFrameAscendColor);
            }

            for (var i = 0; i < synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X + 275 + 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.settings.BarFrameSize.Y,
                        this.settings.BarFrameSize.Y),
                    this.settings.BarFrameDescendColor);
            }
        }

        private void DrawSynchronizationDotFrame()
        {
            // left side frame
            for (var i = 0; i < synchronization.SynchronizedHourMax; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X - 275 - 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.settings.BarFrameSize.Y,
                        this.settings.BarFrameSize.Y),
                    this.settings.SynchronizationDotFrameColor);
            }

            // right side frame
            for (var i = 0; i < synchronization.SynchronizedHourMax; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)this.StateInfoCenter.X + 275 + 15 * i,
                        (int)this.StateInfoCenter.Y - 1,
                        this.settings.BarFrameSize.Y,
                        this.settings.BarFrameSize.Y),
                    this.settings.SynchronizationDotFrameColor);
            }
        }

        #endregion Update and Draw
    }
}