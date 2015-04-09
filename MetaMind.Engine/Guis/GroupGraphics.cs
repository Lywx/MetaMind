namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public interface IGroupGraphics
    {
        void Draw(GameTime gameTime);

        void UpdateStructure(GameTime gameTime);

        void UpdateInput(GameTime gameTime);
    }

    public class GroupGraphics<TGroup, TGroupSettings, TGroupControl> : GroupComponent<TGroup, TGroupSettings, TGroupControl>, IGroupGraphics
        where                  TGroup                                 : Group         <TGroupSettings>
        where                  TGroupControl                          : GroupControl  <TGroup, TGroupSettings, TGroupControl>
    {
        public GroupGraphics(TGroup group)
            : base(group)
        {
        }

        public void Draw(GameTime gameTime)
        {
        }

        public void UpdateStructure(GameTime gameTime)
        {
        }

        public void UpdateInput(GameTime gameTime)
        {
        }
    }
}