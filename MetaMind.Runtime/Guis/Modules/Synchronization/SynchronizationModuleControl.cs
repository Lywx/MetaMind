namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;

    public class SynchronizationModuleControl : ModuleControl<SynchronizationModule, SynchronizationSettings, SynchronizationModuleControl>
    {
        public SynchronizationModuleControl(SynchronizationModule module)
            : base(module)
        {
        }

        public override void LoadContent(IGameInteropService interop)
        {
            
        }

        public override void UnloadContent(IGameInteropService interop)
        {

        }
    }
}