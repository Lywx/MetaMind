namespace MetaMind.Testimony.Sessions
{
    using System.Runtime.Serialization;

    using MetaMind.Testimony.Concepts.Cognitions;
    using MetaMind.Testimony.Concepts.Motivations;

    [DataContract]
    [KnownType(typeof(Cognition))]
    [KnownType(typeof(Experience))]
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