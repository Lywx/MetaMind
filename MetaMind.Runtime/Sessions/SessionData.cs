namespace MetaMind.Runtime.Sessions
{
    using System.Runtime.Serialization;

    using MetaMind.Runtime.Concepts.Cognitions;
    using MetaMind.Runtime.Concepts.Motivations;

    [DataContract]
    public class SessionData : ISessionData
    {
        public SessionData()
        {
            this.Cognition  = new Cognition();
            this.Experience = new Experience();
        }

        [DataMember]
        public ICognition Cognition { get; private set; }

        [DataMember]
        public IExperience Experience { get; private set; }

        public void Update()
        {
            this.Cognition.Update();
        }
    }
}