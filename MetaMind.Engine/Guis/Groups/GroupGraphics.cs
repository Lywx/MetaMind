using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Groups
{
    public interface IGroupGraphics
    {
        void Draw( GameTime gameTime );

        void Update( GameTime gameTime );
    }

    public class GroupGraphics<TGroup, TGroupSettings, TGroupControl> : GroupComponent<TGroup, TGroupSettings, TGroupControl>, IGroupGraphics
        where                  TGroup                                 : Group         <TGroupSettings>
        where                  TGroupControl                          : GroupControl  <TGroup, TGroupSettings, TGroupControl>
    {
        public GroupGraphics( TGroup group )
            : base( group )
        {
        }

        public void Draw( GameTime gameTime )
        {
        }

        public void Update( GameTime gameTime )
        {
        }
    }
}