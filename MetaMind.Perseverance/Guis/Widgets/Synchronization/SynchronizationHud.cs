using System;
using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Guis.Widgets.SynchronizationHuds
{
    public class SynchronizationHud : Widget // refactoring to graphics components
    {
        //---------------------------------------------------------------------
        private Synchronization           synchronization;
        private SynchronizationHudSettings settings;

        //---------------------------------------------------------------------
        private SynchronizationHudSynchronizationStartListener synchronizationStartListener;
        private SynchronizationHudSynchronizationStopListener  synchronizationStopListener;

        private SynchronizationHudMonitor                      monitor;
        
        //---------------------------------------------------------------------

        #region Constructors

        public SynchronizationHud( Synchronization synchronization, SynchronizationHudSettings settings )
        {
            this.synchronization = synchronization;
            this.settings = settings;

            synchronizationStartListener = new SynchronizationHudSynchronizationStartListener( synchronization, this );
            EventManager.AddListener( synchronizationStartListener );

            synchronizationStopListener = new SynchronizationHudSynchronizationStopListener( synchronization, this );
            EventManager.AddListener( synchronizationStopListener );

            monitor = new SynchronizationHudMonitor( ScreenManager.Game );
        }

        #endregion Constructors

        //---------------------------------------------------------------------

        #region Positions

        private Vector2 AccelerationPrefixPosition
        {
            get
            {
                return new Vector2(
                    StatusPosition.X + settings.AccelerationMargin.X,
                    StatusPosition.Y + settings.AccelerationMargin.Y
                    );
            }
        }

        private Vector2 AccelerationSubfixPosition
        {
            get
            {
                const int xSymbolWidth = 43;
                return new Vector2(
                    AccelerationPrefixPosition.X + xSymbolWidth,
                    AccelerationPrefixPosition.Y
                    );
            }
        }

        private Vector2 AccumulationPrefixPosition
        {
            get
            {
                return new Vector2(
                    StatePosition.X + settings.AccumulationMargin.X,
                    StatePosition.Y + settings.AccumulationMargin.Y
                    );
            }
        }

        private Vector2 AccumulationSubfixPosition
        {
            get
            {
                const int plusSymbolWidth = 42;
                return new Vector2(
                    AccumulationPrefixPosition.X + plusSymbolWidth,
                    AccumulationPrefixPosition.Y
                    );
            }
        }

        private Rectangle BackgroundFrameRectangle
        {
            get
            {
                return new Rectangle(
                    settings.BarFrameXC - settings.BarFrameSize.X/2,
                    settings.BarFrameYC - settings.BarFrameSize.Y/2,
                    settings.BarFrameSize.X,
                    settings.BarFrameSize.Y
                    );
            }
        }

        private Vector2 MessagePosition
        {
            get
            {
                return new Vector2(
                    (int) StatePosition.X,
                    GraphicsSettings.Height - 15);
            }
        }

        private Rectangle ProgressFrameRectangle
        {
            get
            {
                return new Rectangle(
                    settings.BarFrameXC - settings.BarFrameSize.X/2,
                    settings.BarFrameYC - settings.BarFrameSize.Y/2,
                    (int) (synchronization.ProgressPercent*settings.BarFrameSize.X),
                    settings.BarFrameSize.Y
                    );
            }
        }

        private Vector2 StatePosition
        {
            get
            {
                return new Vector2(
                    settings.BarFrameXC,
                    settings.BarFrameYC + settings.StateMargin.Y
                    );
            }
        }

        private Vector2 StatusPosition
        {
            get
            {
                return new Vector2(
                    settings.BarFrameXC,
                    settings.BarFrameYC + settings.InformationMargin.Y
                    );
            }
        }

        #endregion

        //---------------------------------------------------------------------

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            DrawProgressFrame();
            DrawStateInformation();
            DrawStatusInformation();
            DrawAccelerationIndicator();
            DrawAccumulationIndicator();
            DrawSynchronizedPointFrame();
            DrawMassage( gameTime );

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.Additive );

            DrawProgressContent();
            DrawSynchronizedPointContent();

            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin();
        }

        public override void UpdateInput( GameTime gameTime )
        {
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        private void DrawAccelerationIndicator()
        {
            FontManager.DrawCenteredText( 
                settings.AccelerationFont, 
                "x", 
                AccelerationPrefixPosition, 
                settings.AccelerationColor, 1f );
            FontManager.DrawCenteredText( 
                settings.AccelerationFont, 
                string.Format( "{0}", synchronization.Acceleration.ToString( "F1" ) ), 
                AccelerationSubfixPosition, 
                settings.AccelerationColor, 
                settings.AccelerationSize );
        }

        private void DrawAccumulationIndicator()
        {
            FontManager.DrawCenteredText(
                settings.AccumulationFont,
                string.Format( "{0}", synchronization.ElapsedTimeSinceTransition.ToString( "hh':'mm':'ss" ) ),
                AccumulationSubfixPosition,
                settings.AccumulationColor,
                settings.AccumulationSize );
        }

        private void DrawMassage( GameTime gameTime )
        {
            var alpha = ( byte ) ( 255 * Math.Abs( Math.Sin( ( gameTime.TotalGameTime.TotalSeconds * 3 ) ) ) );
            if ( synchronization.SynchronizedHourToday >= synchronization.SynchronizedHourYesterday )
            {
                FontManager.DrawCenteredText( 
                    settings.MessageFont, 
                    "Look like you are gonna be more happier from today.", 
                    MessagePosition, 
                    settings.BarFrameAscendColor.MakeTransparent( alpha ),
                    settings.MessageSize );
            }
            else
            {
                FontManager.DrawCenteredText(
                    settings.MessageFont,
                    "Look like you are gonna be less happier from today.",
                    MessagePosition,
                    settings.BarFrameDescendColor.MakeTransparent( alpha ),
                    settings.MessageSize );
            }
        }

        private void DrawProgressContent()
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, ProgressFrameRectangle, synchronization.Enabled ? settings.BarFrameAscendColor : settings.BarFrameDescendColor );
        }

        private void DrawProgressFrame()
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, BackgroundFrameRectangle, settings.BarFrameBackgroundColor );
        }

        private void DrawStateInformation()
        {
            FontManager.DrawCenteredText( settings.StateFont, synchronization.Enabled ? "Synchronizing" : "Losing Synchronicity", StatePosition, settings.StateColor, settings.StateSize );
        }

        private void DrawStatusInformation()
        {
            FontManager.DrawCenteredText( settings.StateFont, string.Format( "Level {0}: {1}", synchronization.Level, synchronization.State ), StatusPosition, settings.StatusColor, settings.StatusSize );
        }

        private void DrawSynchronizedPointContent()
        {
            // left side content
            for ( var i = 0 ; i < synchronization.SynchronizedHourToday ; ++i )
            {
                Primitives2D.FillCenterRectangle( ScreenManager.SpriteBatch, new Rectangle( ( int ) StatePosition.X - 275 - 15 * i, ( int ) StatePosition.Y - 1, settings.BarFrameSize.Y, settings.BarFrameSize.Y ), settings.BarFrameAscendColor );
            }
            for ( var i = 0 ; i < synchronization.SynchronizedHourYesterday ; ++i )
            {
                Primitives2D.FillCenterRectangle( ScreenManager.SpriteBatch, new Rectangle( ( int ) StatePosition.X - 275 - 15 * i, ( int ) StatePosition.Y - 1, settings.BarFrameSize.Y, settings.BarFrameSize.Y ), settings.BarFrameDescendColor );
            }
            // right side content
            for ( var i = 0 ; i < synchronization.SynchronizedHourToday ; ++i )
            {
                Primitives2D.FillCenterRectangle( ScreenManager.SpriteBatch, new Rectangle( ( int ) StatePosition.X + 275 + 15 * i, ( int ) StatePosition.Y - 1, settings.BarFrameSize.Y, settings.BarFrameSize.Y ), settings.BarFrameAscendColor );
            }
            for ( var i = 0 ; i < synchronization.SynchronizedHourYesterday ; ++i )
            {
                Primitives2D.FillCenterRectangle( ScreenManager.SpriteBatch, new Rectangle( ( int ) StatePosition.X + 275 + 15 * i, ( int ) StatePosition.Y - 1, settings.BarFrameSize.Y, settings.BarFrameSize.Y ), settings.BarFrameDescendColor );
            }
        }

        private void DrawSynchronizedPointFrame()
        {
            // left side frame
            for ( var i = 0 ; i < synchronization.SynchronizedHourMax ; ++i )
            {
                Primitives2D.FillCenterRectangle( ScreenManager.SpriteBatch, new Rectangle( ( int ) StatePosition.X - 275 - 15 * i, ( int ) StatePosition.Y - 1, settings.BarFrameSize.Y, settings.BarFrameSize.Y ), settings.HourFrameColor );
            }
            // right side frame
            for ( var i = 0 ; i < synchronization.SynchronizedHourMax ; ++i )
            {
                Primitives2D.FillCenterRectangle( ScreenManager.SpriteBatch, new Rectangle( ( int ) StatePosition.X + 275 + 15 * i, ( int ) StatePosition.Y - 1, settings.BarFrameSize.Y, settings.BarFrameSize.Y ), settings.HourFrameColor );
            }
        }

        #endregion Update and Draw

        #region Operations

        public void StartSynchronizing( TaskEntry target )
        {
            synchronization.Start( target );
            monitor.Activate();
        }

        public void StopSynchronizing()
        {
            synchronization.Stop();
            monitor.Deactivate();
        }

        #endregion Operations
    }
}