using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public interface IModuleGraphics
    {
        void Draw( GameTime gameTime );

        void Update( GameTime gameTime );
    }

    public abstract class ModuleGraphics<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleGraphics
        where TModule : Module<TModuleSettings>
        where TModuleControl : ModuleControl<TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleGraphics( TModule module )
            : base( module )
        {
        }

        public abstract void Draw( GameTime gameTime );

        public abstract void Update( GameTime gameTime );
    }
}