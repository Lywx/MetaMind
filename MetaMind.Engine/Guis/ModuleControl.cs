namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public interface IModuleControl
    {
        void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound);

        void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound);

        void Update(IGameInput gameInput, GameTime gameTime);

        void Update(GameTime gameTime);
    }

    public abstract class ModuleControl<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleControl
        where                           TModule                                   : Module         <TModuleSettings>
        where                           TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleControl(TModule module)
            : base(module)
        {
        }

        public abstract void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound);

        public abstract void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound);
    }
}