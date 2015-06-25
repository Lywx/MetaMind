namespace MetaMind.Testimony.Sessions
{
    using System.Runtime.Serialization;
    using Concepts.Operations;
    using Concepts.Tests;
    using MetaMind.Testimony.Concepts.Cognitions;

    [DataContract]
    [KnownType(typeof(Cognition))]
    public class SessionData : ISessionData
    {
        public SessionData()
        {
            this.Cognition = new Cognition();

            this.Reset();
        }

        [DataMember]
        public ICognition Cognition { get; private set; }

        public IOperation Operations { get; private set; }

        public ITest Test { get; private set; }

        public void Update()
        {
            this.Cognition.Update();
            this.Test     .Update();
        }

        private void Reset()
        {
            this.Test = new Test("Root", "", "");
        }

        [OnDeserialized]
        private void Reset(StreamingContext context)
        {
            this.Reset();
        }
    }
}