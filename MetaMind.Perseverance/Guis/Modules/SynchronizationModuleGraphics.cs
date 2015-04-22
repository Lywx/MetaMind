namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Settings.Colors;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class SynchronizationModuleGraphics : ModuleGraphics<SynchronizationModule, SynchronizationModuleSettings, SynchronizationModuleControl>
    {
        private string StateInfoTrue  = "Gaining Synchronicity"; 

        private string StateInfoFalse = "Losing Synchronicity";

        private string MessageInfoIncrease = "Computational Motivation Synchronization Ratio increases";
        
        private string MessageInfoDecrease = "Computational Motivation Synchronization Ratio decreases";

        private Label accumulationInfo;
        
        private Label stateInfo;
        private Label statusInfo;

        private Label messageInfo;

        private Label accelerationInfoSubfix;
        private Label accelerationInfoPrefix;

        private ISynchronization synchronization;

        public SynchronizationModuleGraphics(SynchronizationModule module)
            : base(module)
        {
            this.synchronization = this.Module.Synchronization;

            this.stateInfo = new Label(
                () => Font.UiStatistics,
                () => this.synchronization.Enabled ? this.StateInfoTrue : this.StateInfoFalse,
                () => this.StateInfoCenter,
                () => Color.White,
                () => 1.1f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            this.statusInfo = new Label(
                () => Font.UiStatistics,
                () => string.Format("Level {0}: {1}", this.synchronization.Level, this.synchronization.State),
                () => this.StatusInfoCenter,
                () => Color.White,
                () => 0.7f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            this.accumulationInfo = new Label(
                () => Font.UiStatistics,
                () => string.Format("{0}", this.synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                () => this.AccumulationSubfixCenter,
                () => Color.White,
                () => 0.7f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            this.accelerationInfoPrefix = new Label(
                () => Font.UiStatistics,
                () => "x",
                () => this.AccelerationPrefixCenter,
                () => Color.White,
                () => 1f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            this.accelerationInfoSubfix = new Label(
                () => Font.UiStatistics,
                () => string.Format("{0}", this.synchronization.Acceleration.ToString("F1")),
                () => this.AccelerationSubfixCenter,
                () => Color.White,
                () => 2.0f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            Func<byte> alpha  = () => (byte)(255 * Math.Abs(Math.Sin(time.TotalGameTime.TotalSeconds * 3)));
            Func<bool> better = () => this.synchronization.SynchronizedHourToday >= this.synchronization.SynchronizedHourYesterday;

            this.messageInfo = new Label(
                () => Font.UiStatistics,
                () => better() ? this.MessageInfoIncrease : this.MessageInfoDecrease,
                () => this.MessageCenter,
                () => (better() ? Palette.LightBlue : Palette.LightPink).MakeTransparent(alpha()),
                () => 0.7f,
                StringHAlign.Center,
                StringVAlign.Center,
                false);
        }

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
                return new Vector2((int)this.StateInfoCenter.X, this.Settings.ScreenHeight - 15);
            }
        }

        private Rectangle ProgressBarRectangle
        {
            get
            {
                return new Rectangle(
                    this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                    this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                    (int)(synchronization.ProgressPercent * this.Settings.BarFrameSize.X),
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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawProgressFrame();
            this.DrawProgressBar();
            this.DrawDotFrame();
            this.DrawDot();

            this.stateInfo.Draw(graphics, time, alpha);
            this.statusInfo.Draw(graphics, time, alpha);

            this.accelerationInfoPrefix.Draw(graphics, time, alpha);
            this.accelerationInfoSubfix.Draw(graphics, time, alpha);

            this.accumulationInfo.Draw(graphics, time, alpha);
            
            this.DrawDailyRateInfo();
            
            this.messageInfo.Draw(graphics, time, alpha);
        }

        private void DrawDailyRateInfo()
        {
            var awake = this.cognition.Consciousness as ConsciousnessAwake;
            if (awake != null)
            {
                var awakeTime = awake.AwakeSpan;

                var synchronizedTime = synchronization.SynchronizedTimeToday
                                       + (synchronization.Enabled
                                              ? synchronization.ElapsedTimeSinceTransition
                                              : TimeSpan.Zero);

                var syncRate = synchronizedTime.TotalSeconds / awakeTime.TotalSeconds;
                var syncRateText = (syncRate * 100).ToString("F0");

                // draw rate digits
                FontManager.DrawStringCenteredHV(
                    this.Settings.SynchronizationRateFont,
                    syncRateText,
                    this.DailySyncHourPrefixCenter,
                    this.Settings.SynchronizationRateColor,
                    this.Settings.SynchronizationRateSize);

                const int    SymbolMargin = 10;
                const string Symbol = "%";

                var syncRateTextWidth = FontManager.MeasureString(this.Settings.SynchronizationRateFont, syncRateText, this.Settings.SynchronizationRateSize).X;
                var syncRateTextMargin = new Vector2(syncRateTextWidth / 2 * this.Settings.SynchronizationRateSize + SymbolMargin, 0);

                // draw % after rate digits
                FontManager.DrawStringCenteredHV(
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
            var barFrameAscendColor = Palette.LightBlue;
            for (var i = 0; i < synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    this.GameGraphics.SpriteBatch,
                    this.SynchronizationDotRectangle(i, true),
                    barFrameAscendColor);
            }

            var barFrameDescendColor = Palette.LightPink;
            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    this.GameGraphics.SpriteBatch,
                    this.SynchronizationDotRectangle(i, true),
                    barFrameDescendColor);
            }

            // right side content
            for (var i = 0; i < this.synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    this.GameGraphics.SpriteBatch,
                    this.SynchronizationDotRectangle(i, false),
                    barFrameAscendColor);
            }

            for (var i = 0; i < this.synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    this.GameGraphics.SpriteBatch,
                    this.SynchronizationDotRectangle(i, false),
                    barFrameDescendColor);
            }
        }

        private void DrawDotFrame()
        {
            // left side frame
            for (var i = 0; i < synchronization.SynchronizedHourMax; ++i)
            {
                var dotFrame = this.SynchronizationDotRectangle(i, true);

                Primitives2D.FillRectangle(
                    this.GameGraphics.SpriteBatch,
                    dotFrame,
                    this.Settings.SynchronizationDotFrameColor);

                FontManager.DrawStringCenteredHV(
                    this.Settings.SynchronizationRateFont,
                    ((i + 1) % 10).ToString("F0"),
                    this.SynchronizationDotTextCenter(dotFrame),
                    this.Settings.SynchronizationRateColor,
                    0.7f);
            }

            // right side frame
            for (var i = 0; i < synchronization.SynchronizedHourMax; ++i)
            {
                var dotFrame = this.SynchronizationDotRectangle(i, false);

                Primitives2D.FillRectangle(
                    this.GameGraphics.SpriteBatch,
                    dotFrame,
                    this.Settings.SynchronizationDotFrameColor);

                FontManager.DrawStringCenteredHV(
                    this.Settings.SynchronizationRateFont,
                    ((i + 1) % 10).ToString("F0"),
                    this.SynchronizationDotTextCenter(dotFrame),
                    this.Settings.SynchronizationRateColor,
                    0.7f);
            }
        }

        private void DrawProgressBar()
        {
            Primitives2D.FillRectangle(
                this.GameGraphics.SpriteBatch,
                this.ProgressBarRectangle,
                this.synchronization.Enabled ? Palette.LightBlue : Palette.LightPink);
        }

        private void DrawProgressFrame()
        {
            Primitives2D.FillRectangle(
                this.GameGraphics.SpriteBatch,
                this.ProgressFrameRectangle,
                this.Settings.BarFrameBackgroundColor);
        }

        private Rectangle SynchronizationDotRectangle(int i, bool leftsided)
        {
            return ExtRectangle.Rectangle(
                leftsided ? (int)this.StateInfoCenter.X - 275 - 15 * i : (int)this.StateInfoCenter.X + 275 + 15 * i,
                (int)this.StateInfoCenter.Y - 1,
                this.Settings.BarFrameSize.Y,
                this.Settings.BarFrameSize.Y);
        }

        private Vector2 SynchronizationDotTextCenter(Rectangle dotFrame)
        {
            return dotFrame.Center.ToVector2() + new Vector2(0, this.Settings.BarFrameSize.Y * 2);
        }
    }
}