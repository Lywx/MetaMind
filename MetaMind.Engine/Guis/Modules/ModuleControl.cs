using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Modules
{
    public interface IModuleControl
    {
        void HandleInput();
        void Load();
        void Unload();
        void UpdateInput( GameTime gameTime );
        void UpdateStructure( GameTime gameTime );
    }

    public abstract class ModuleControl<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleControl
        where                           TModule                                   : Module         <TModuleSettings>
        where                           TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleControl( TModule module )
            : base( module )
        {
        }

        public abstract void HandleInput();
        public abstract void Load();
        public abstract void Unload();
        public abstract void UpdateInput(GameTime gameTime);
        public abstract void UpdateStructure(GameTime gameTime);
    }
}