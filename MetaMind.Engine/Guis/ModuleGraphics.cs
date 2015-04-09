namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public interface IModuleGraphics
    {
        void Draw(GameTime gameTime);

        void UpdateStructure(GameTime gameTime);

        void UpdateInput(IGameInput gameInput, GameTime gameTime);
    }

    public abstract class ModuleGraphics<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleGraphics
        where                            TModule                                   : Module         <TModuleSettings>
        where                            TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleGraphics(TModule module)
            : base(module)
        {
        }

        public abstract void Draw(GameTime gameTime);

        public abstract void UpdateStructure(GameTime gameTime);

        public abstract void UpdateInput(IGameInput gameInput, GameTime gameTime);
    }
}