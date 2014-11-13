using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MetaMind.Perseverance.Guis.Widgets.Synchronizations
{
    public class SynchronizationHud : Widget
    {
        private ISynchronization                               synchronization;
        private SynchronizationHudSettings                     settings;

        private SynchronizationHudSynchronizationStartListener synchronizationStartListener;
        private SynchronizationHudSynchronizationStopListener  synchronizationStopListener;

        private SynchronizationHudMonitor                      monitor;

        #region Constructors

        public SynchronizationHud(ISynchronization synchronization, SynchronizationHudSettings settings)
        {
            this.synchronization = synchronization;
            this.settings = settings;

            synchronizationStartListener = new SynchronizationHudSynchronizationStartListener(synchronization, this);
            EventManager.AddListener(synchronizationStartListener);

            synchronizationStopListener = new SynchronizationHudSynchronizationStopListener(synchronization, this);
            EventManager.AddListener(synchronizationStopListener);

            monitor = new SynchronizationHudMonitor(ScreenManager.Game);
        }

        #endregion Constructors

        #region Locations

        private Vector2 AccelerationPrefixLocation
        {
            get
            {
                return new Vector2(
                    StatusLocation.X + settings.AccelerationMargin.X,
                    StatusLocation.Y + settings.AccelerationMargin.Y);
            }
        }

        private Vector2 AccelerationSubfixLocation
        {
            get
            {
                const int XSymbolWidth = 43;
                return new Vector2(
                    AccelerationPrefixLocation.X + XSymbolWidth,
                    AccelerationPrefixLocation.Y);
            }
        }

        private Vector2 AccumulationPrefixLocation
        {
            get
            {
                return new Vector2(
                    StateLocation.X + settings.AccumulationMargin.X,
                    StateLocation.Y + settings.AccumulationMargin.Y);
            }
        }

        private Vector2 AccumulationSubfixLocation
        {
            get
            {
                const int PlusSymbolWidth = 42;
                return new Vector2(
                    AccumulationPrefixLocation.X + PlusSymbolWidth,
                    AccumulationPrefixLocation.Y);
            }
        }

        private Rectangle BackgroundFrameRectangle
        {
            get
            {
                return new Rectangle(
                    settings.BarFrameXC - settings.BarFrameSize.X / 2,
                    settings.BarFrameYC - settings.BarFrameSize.Y / 2,
                    settings.BarFrameSize.X,
                    settings.BarFrameSize.Y);
            }
        }

        private Vector2 MessageLocation
        {
            get
            {
                return new Vector2(
                    (int)StateLocation.X,
                    GraphicsSettings.Height - 15);
            }
        }

        private Rectangle ProgressFrameRectangle
        {
            get
            {
                return new Rectangle(
                    settings.BarFrameXC - settings.BarFrameSize.X / 2,
                    settings.BarFrameYC - settings.BarFrameSize.Y / 2,
                    (int)(synchronization.ProgressPercent * settings.BarFrameSize.X),
                    settings.BarFrameSize.Y);
            }
        }

        private Vector2 StateLocation
        {
            get
            {
                return new Vector2(
                    settings.BarFrameXC,
                    settings.BarFrameYC + settings.StateMargin.Y);
            }
        }

        private Vector2 StatusLocation
        {
            get
            {
                return new Vector2(
                    settings.BarFrameXC,
                    settings.BarFrameYC + settings.InformationMargin.Y);
            }
        }

        #endregion Locations

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            DrawProgressFrame();
            DrawStateInformation();
            DrawStatusInformation();
            DrawAccelerationIndicator();
            DrawAccumulationIndicator();
            DrawSynchronizedPointFrame();
            DrawMassage(gameTime);

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);

            DrawProgressContent();
            DrawSynchronizedPointContent();

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin();
        }

        public override void UpdateInput(GameTime gameTime)
        {
        }

        public override void UpdateStructure(GameTime gameTime)
        {
        }

        private void DrawAccelerationIndicator()
        {
            FontManager.DrawCenteredText(
                settings.AccelerationFont,
                "x",
                AccelerationPrefixLocation,
                settings.AccelerationColor,
                1f);
            FontManager.DrawCenteredText(
                settings.AccelerationFont,
                string.Format("{0}", synchronization.Acceleration.ToString("F1")),
                AccelerationSubfixLocation,
                settings.AccelerationColor,
                settings.AccelerationSize);
        }

        private void DrawAccumulationIndicator()
        {
            FontManager.DrawCenteredText(
                settings.AccumulationFont,
                string.Format("{0}", synchronization.ElapsedTimeSinceTransition.ToString("hh':'mm':'ss")),
                AccumulationSubfixLocation,
                settings.AccumulationColor,
                settings.AccumulationSize);
        }

        private void DrawMassage(GameTime gameTime)
        {
            var alpha  = (byte)(255 * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3)));
            var better = synchronization.SynchronizedHourToday >= synchronization.SynchronizedHourYesterday;

            const string HappyNotice   = "Look like you are gonna be more happier from today.";
            const string UnhappyNotice = "Look like you are gonna be less happier from today.";

            FontManager.DrawCenteredText(settings.MessageFont, better ? HappyNotice : UnhappyNotice, MessageLocation, (better ? settings.BarFrameAscendColor : settings.BarFrameDescendColor).MakeTransparent(alpha), settings.MessageSize);
        }

        private void DrawProgressContent()
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, ProgressFrameRectangle, synchronization.Enabled ? settings.BarFrameAscendColor : settings.BarFrameDescendColor);
        }

        private void DrawProgressFrame()
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, BackgroundFrameRectangle, settings.BarFrameBackgroundColor);
        }

        private void DrawStateInformation()
        {
            const string SyncTrueInfo  = "Synchronizing";
            const string SyncFalseInfo = "Losing Synchronicity";
            FontManager.DrawCenteredText(settings.StateFont, synchronization.Enabled ? SyncTrueInfo : SyncFalseInfo, StateLocation, settings.StateColor, settings.StateSize);
        }

        private void DrawStatusInformation()
        {
            FontManager.DrawCenteredText(settings.StateFont, string.Format("Level {0}: {1}", synchronization.Level, synchronization.State), StatusLocation, settings.StatusColor, settings.StatusSize);
        }

        private void DrawSynchronizedPointContent()
        {
            // left side content
            for (var i = 0; i < synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)StateLocation.X - 275 - 15 * i,
                        (int)StateLocation.Y - 1,
                        settings.BarFrameSize.Y,
                        settings.BarFrameSize.Y),
                    settings.BarFrameAscendColor);
            }

            for (var i = 0; i < synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)StateLocation.X - 275 - 15 * i,
                        (int)StateLocation.Y - 1,
                        settings.BarFrameSize.Y,
                        settings.BarFrameSize.Y),
                    settings.BarFrameDescendColor);
            }

            // right side content
            for (var i = 0; i < synchronization.SynchronizedHourToday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)StateLocation.X + 275 + 15 * i,
                        (int)StateLocation.Y - 1,
                        settings.BarFrameSize.Y,
                        settings.BarFrameSize.Y),
                    settings.BarFrameAscendColor);
            }

            for (var i = 0; i < synchronization.SynchronizedHourYesterday; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)StateLocation.X + 275 + 15 * i,
                        (int)StateLocation.Y - 1,
                        settings.BarFrameSize.Y,
                        settings.BarFrameSize.Y),
                    settings.BarFrameDescendColor);
            }
        }

        private void DrawSynchronizedPointFrame()
        {
            // left side frame
            for (var i = 0; i < synchronization.SynchronizedHourMax; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)StateLocation.X - 275 - 15 * i,
                        (int)StateLocation.Y - 1,
                        settings.BarFrameSize.Y,
                        settings.BarFrameSize.Y),
                    settings.HourFrameColor);
            }

            // right side frame
            for (var i = 0; i < synchronization.SynchronizedHourMax; ++i)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Rectangle(
                        (int)StateLocation.X + 275 + 15 * i,
                        (int)StateLocation.Y - 1,
                        settings.BarFrameSize.Y,
                        settings.BarFrameSize.Y),
                    settings.HourFrameColor);
            }
        }

        #endregion Update and Draw

        #region Operations

        public void StartSynchronizing(TaskEntry target)
        {
            synchronization.TryStart(target);
            monitor        .TryStart();
        }

        public void StopSynchronizing()
        {
            synchronization.Stop();
            monitor        .Stop();
        }

        #endregion Operations
    }
}