using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Groups
{
    public interface IGroupControl
    {
        void HandleInput();

        void UpdateInput( GameTime gameTime );

        void UpdateStructure( GameTime gameTime );
    }

    public class GroupControl<TGroup, TGroupSettings, TGroupControl> : GroupComponent<TGroup, TGroupSettings, TGroupControl>, IGroupControl
        where                 TGroup                                 : Group         <TGroupSettings>
        where                 TGroupControl                          : GroupControl  <TGroup, TGroupSettings, TGroupControl>
    {
        public GroupControl( TGroup group )
            : base( group )
        {
        }

        public void HandleInput()
        {
        }

        public void UpdateInput( GameTime gameTime )
        {
        }

        public void UpdateStructure( GameTime gameTime )
        {
        }
    }
}