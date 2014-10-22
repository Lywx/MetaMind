using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.TimelineHuds
{
    public class TimelineHud
    {
        private Vector2 position;

        private TimelineText pastSymbol;
        private TimelineText nowSymbol;
        private TimelineText futureSymbol;

        private TimelineLight light;

        public TimelineHud( Vector2 position )
        {
            this.position = position;

            light        = new TimelineLight( new Point( ( int ) position.X, ( int ) position.Y + 10 ) );
            
            pastSymbol   = new TimelineText( "Past",   position );
            nowSymbol    = new TimelineText( "Now",    position + new Vector2( 260, 0 ) );
            futureSymbol = new TimelineText( "Future", position + new Vector2( 250, 0 ) * 2 );
        }

        public void Draw( GameTime gameTime )
        {
            light       .Draw( gameTime );
            pastSymbol  .Draw( gameTime );
            nowSymbol   .Draw( gameTime );
            futureSymbol.Draw( gameTime );
        }

        public void Update( GameTime gameTime )
        {
            light       .Update( gameTime );
            pastSymbol  .Update( gameTime );
            nowSymbol   .Update( gameTime );
            futureSymbol.Update( gameTime );
        }
    }
}