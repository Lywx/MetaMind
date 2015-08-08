namespace MetaMind.Unity.Sessions
{
    using System.Runtime.Serialization;
    using Concepts.Cognitions;
    using Concepts.Operations;
    using Concepts.Tests;

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

        public IOperationDescription Operation { get; private set; }

        public ITest Test { get; private set; }

        public void Update()
        {
            this.Cognition.Update();

            this.Test     .Update();
            this.Operation.Update();
        }

        private void Reset()
        {
            this.Test = new Test("Root", "", "");
            this.Operation = new OperationDescription("Root", "", "");
        }

        [OnDeserialized]
        private void Reset(StreamingContext context)
        {
            this.Reset();
        }
    }
}