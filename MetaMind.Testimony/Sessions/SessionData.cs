namespace MetaMind.Testimony.Sessions
{
    using System.Runtime.Serialization;
    using Concepts.Tests;
    using MetaMind.Testimony.Concepts.Cognitions;

    [DataContract]
    [KnownType(typeof(Cognition))]
    [KnownType(typeof(Test))]
    public class SessionData : ISessionData
    {
        public SessionData()
        {
            this.Cognition = new Cognition();

            this.Test = new Test("Root", "Root of tests");
        }

        [DataMember]
        public ICognition Cognition { get; private set; }

        [DataMember]
        public ITest Test { get; private set; }

        public void Update()
        {
            this.Cognition.Update();
            this.Test     .Update();
        }
    }
}