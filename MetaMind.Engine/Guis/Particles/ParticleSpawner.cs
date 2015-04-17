namespace MetaMind.Engine.Guis.Particles
{
    public class ParticleSpawner<TParticle> where TParticle : IRandomParticle
    {
        private readonly TParticle prototype;

        public ParticleSpawner(TParticle prototype)
        {
            this.prototype = prototype;
        }

        public IRandomParticle Spawn()
        {
            return ((TParticle)this.prototype.Clone()).Randomize();
        }
    }
}