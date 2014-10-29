using C3.Primtive2DXna;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Concepts.Cognitions;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class SummaryModule : Module<SummaryModuleSettings>
    {
        //---------------------------------------------------------------------
        private SummaryModuleSettings settings;
        
        private IConsciousness   consciousness;
        private Synchronization synchronization;
        
        //---------------------------------------------------------------------
        private bool enabled;

        #region Constructors

        public SummaryModule( Synchronization synchronization, IConsciousness consciousness, SummaryModuleSettings settings )
        {
            this.settings = settings;

            this.synchronization = synchronization;
            this.consciousness = consciousness;
        }

        #endregion Constructors

        //---------------------------------------------------------------------

        private Vector2 TitleCenterPosition
        {
            get { return new Vector2( GraphicsSettings.Width / 2f, 100 ); }
        }

        //---------------------------------------------------------------------

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if ( !enabled )
                return;

            DrawSummaryTitle ( "Summary",                                                                              Color.White );
            DrawSummaryEntity( "Hours in Synchronization:",  0, synchronization.SynchronizedHourYesterday.ToSummary(), Color.White );
            DrawSummaryEntity( "Hours in Good Profession:",  1, -settings.GoodPrefessionHour,                          Color.Red );
            DrawSummaryEntity( "Hours in Lofty Profession:", 2, -settings.LoftyProfessionHour,                         Color.Red );
            DrawSummarySplit (                               3,                                                        Color.Red );
            DrawSummaryResult( string.Empty,                 4,                                                        Color.White, Color.Red );
        }

        public override void UpdateInput( GameTime gameTime )
        {
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            enabled = !consciousness.AwakeCondition;
        }

        private void DrawSummaryEntity( string caption, int line, object presentedData, Color color )
        {
            var captionPosition = new Vector2( GraphicsSettings.Width / 2f - 300, 150 + line * settings.LineHeight );
            var contentPosition = new Vector2( GraphicsSettings.Width / 2f + 270, 150 + line * settings.LineHeight );
            var contentString   = string.Format( "{0}", presentedData );

            FontManager.DrawText( settings.EntityFont, caption, captionPosition, color, settings.EntitySize );
            FontManager.DrawText( settings.EntityFont, contentString, contentPosition, color, settings.EntitySize );
        }

        private void DrawSummaryResult( string caption, int line, Color goodColor, Color badColor )
        {
            var leftHour = synchronization.SynchronizedHourYesterday - settings.GoodPrefessionHour - settings.LoftyProfessionHour;
            DrawSummaryEntity( caption, line, leftHour.ToSummary(), leftHour >= 0 ? goodColor : badColor );
        }
        private void DrawSummarySplit( int line, Color color )
        {
            var splitStart = new Vector2( GraphicsSettings.Width / 2f - 300, 150 + line * settings.LineHeight + settings.LineHeight / 2 );
            var splitEnd   = new Vector2( GraphicsSettings.Width / 2f + 300, 150 + line * settings.LineHeight + settings.LineHeight / 2 );

            ScreenManager.SpriteBatch.DrawLine( splitStart, splitEnd, color, settings.EntitySize );
        }

        private void DrawSummaryTitle( string title, Color color )
        {
            FontManager.DrawCenteredText( settings.TitleFont, title, TitleCenterPosition, color, settings.TitleSize );
        }

        #endregion Update and Draw

        //---------------------------------------------------------------------
        
    }

    internal static class Int32Extension
    {
        public static string ToSummary( this int hour )
        {
            return hour.ToString( "+#;-#;+0" );
        }
    }
}