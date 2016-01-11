namespace MetaMind.Session.Model.Runtime
{
    using Attention;

    [DataContract]
    [KnownType(typeof(Consciousness))]
    [KnownType(typeof(SynchronizationData))]
    public class Cognition : ICognition
    {
        #region Constructors

        public Cognition()
        {
            this.Consciousness = new Consciousness();
            this.SynchronizationData = new SynchronizationData();
        }

        #endregion Constructors

        [DataMember]
        public IConsciousness Consciousness { get; set; }

        [DataMember]
        public ISynchronizationData SynchronizationData { get; set; }

        public float SynchronizationRate
        {
            get
            {
                if (this.Consciousness.IsAwake)
                {
                    var synchronizationSeconds =
                        this.SynchronizationData.SynchronizedTimeTodayBestCase.
                             TotalSeconds;

                    var awakenSeconds =
                        ((IConsciousnessAwake)this.Consciousness.State).
                            AwakeSpan.TotalSeconds;

                    return (float)(synchronizationSeconds / awakenSeconds * 100);
                }

                return 0;
            }
        }

        #region Update

        public void Update()
        {
            this.Consciousness.Update();
            this.SynchronizationData.Update();
        }

        #endregion Update
    }
}