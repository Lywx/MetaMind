namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Guis.Modules;

    public class ParticleModule : Engine.Guis.Modules.ParticleModule
    {
        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
            this.SpawnSpeed = 2;
        }
    }
}