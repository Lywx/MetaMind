using C3.Primtive2DXna;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Concepts.Cognitions;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class SummaryModule : Module<SummaryModuleSettings>
    {
        #region Constructors

        public SummaryModule( SummaryModuleSettings settings )
            : base( settings )
        {
        }

        #endregion Constructors

        #region Source

        private Consciousness Consciousness
        {
            get { return Perseverance.Adventure.Cognition.Consciousness; }
        }

        private Synchronization Synchronization
        {
            get { return Perseverance.Adventure.Cognition.Synchronization; }
        }


        #endregion

        #region Graphical Position

        private Vector2 TitleCenter
        {
            get { return new Vector2(GraphicsSettings.Width/2f, 100); }
        }

        #endregion

        #region Update and Draw

        public override void Draw( GameTime gameTime, byte alpha )
        {
            if ( Consciousness.AwakeCondition )
                return;

            DrawSummaryTitle ( "Summary",                                                                              Color.White );
            DrawSummaryEntity( "Hours in Synchronization:",  0, Synchronization.SynchronizedHourYesterday.ToSummary(), Color.White );
            DrawSummaryEntity( "Hours in Good Profession:",  1, -Settings.GoodPrefessionHour,                          Color.Red );
            DrawSummaryEntity( "Hours in Lofty Profession:", 2, -Settings.LoftyProfessionHour,                         Color.Red );
            DrawSummarySplit (                               3,                                                        Color.Red );
            DrawSummaryResult( string.Empty,                 4,                                                        Color.White, Color.Red );
        }

        public override void UpdateInput( GameTime gameTime )
        {
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        private void DrawSummaryEntity( string caption, int line, object presentedData, Color color )
        {
            var captionPosition = new Vector2( GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight );
            var contentPosition = new Vector2( GraphicsSettings.Width / 2f + 270, 150 + line * Settings.LineHeight );
            var contentString   = string.Format( "{0}", presentedData );

            FontManager.DrawText( Settings.EntityFont, caption, captionPosition, color, Settings.EntitySize );
            FontManager.DrawText( Settings.EntityFont, contentString, contentPosition, color, Settings.EntitySize );
        }

        private void DrawSummaryResult( string caption, int line, Color goodColor, Color badColor )
        {
            var leftHour = Synchronization.SynchronizedHourYesterday - Settings.GoodPrefessionHour - Settings.LoftyProfessionHour;
            DrawSummaryEntity( caption, line, leftHour.ToSummary(), leftHour >= 0 ? goodColor : badColor );
        }
        private void DrawSummarySplit( int line, Color color )
        {
            var splitStart = new Vector2( GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2 );
            var splitEnd   = new Vector2( GraphicsSettings.Width / 2f + 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2 );

            ScreenManager.SpriteBatch.DrawLine( splitStart, splitEnd, color, Settings.EntitySize );
        }

        private void DrawSummaryTitle( string title, Color color )
        {
            FontManager.DrawCenteredText( Settings.TitleFont, title, TitleCenter, color, Settings.TitleSize );
        }

        #endregion Update and Draw
        
    }

    internal static class Int32Ext
    {
        public static string ToSummary( this int hour )
        {
            return hour.ToString( "+#;-#;+0" );
        }
    }
}