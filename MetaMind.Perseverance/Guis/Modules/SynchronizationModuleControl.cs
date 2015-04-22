namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;

    public class SynchronizationModuleControl : ModuleControl<SynchronizationModule, SynchronizationModuleSettings, SynchronizationModuleControl>
    {

        public SynchronizationModuleControl(SynchronizationModule module)
            : base(module)
        {
        }

        public override void Load(IGameInteropService interop)
        {
            
        }

        public override void Unload(IGameInteropService interop)
        {

        }
    }
}