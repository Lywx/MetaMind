namespace MetaMind.Engine.Entities.Particles
{
    public class MMParticleSpawner<TParticle> where TParticle : IMMRandomParticle
    {
        private readonly TParticle prototype;

        public MMParticleSpawner(TParticle prototype)
        {
            this.prototype = prototype;
        }

        public IMMRandomParticle Spawn()
        {
            return ((TParticle)this.prototype.Clone()).Randomize();
        }
    }
}