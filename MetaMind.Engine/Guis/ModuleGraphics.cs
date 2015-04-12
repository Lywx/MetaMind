namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public abstract class ModuleGraphics<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleGraphics
        where                            TModule                                   : Module         <TModuleSettings>
        where                            TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleGraphics(TModule module)
            : base(module)
        {
        }

        public abstract void Draw(GameTime gameTime);
    }
}