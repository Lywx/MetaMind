using MetaMind.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MetaMind.Perseverance.Guis.Particles
{
    public class FloatParticleContainer : EngineObject
    {
        private int                 particleNum;
        private List<FloatParticle> particles = new List<FloatParticle>();

        public FloatParticleContainer( int particleNum )
        {
            this.particleNum = particleNum;
        }

        public void Draw( GameTime gameTime )
        {
            for ( var i = 0 ; i < particles.Count ; ++i )
            {
                if ( i == particles.Count / 2 )
                {
                    ScreenManager.SpriteBatch.End();
                    ScreenManager.SpriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.Additive );
                }
                particles[ i ].Draw( gameTime );
            }
        }

        public void Update( GameTime gameTime )
        {
            for ( var i = 0 ; i < particles.Count ; ++i )
            {
                particles[ i ].Update( gameTime );
                if ( particles[ i ].LastingSeconds < 0 ||
                    particles[ i ].IsOutsideScreen )
                    particles.Remove( particles[ i ] );
            }
            if ( particles.Count < particleNum )
            {
                particles.Add( FloatParticle.RandParticle() );
            }
        }
    }
}