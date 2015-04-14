namespace MetaMind.Engine.Guis.Particles
{
    public class ParticleSpawner<TParticle> where TParticle : IShapelessParticle
    {
        private TParticle prototype;

        public ParticleSpawner(TParticle prototype)
        {
            this.prototype = prototype;
        }

        public TParticle Spawn()
        {
            return (TParticle)this.prototype.Clone();
        }

        public TParticle Random()
        {
            return 
        }
    }
}