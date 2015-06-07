namespace MetaMind.Acutance.Sessions
{
    using System.Runtime.Serialization;

    using Concepts;
    using Engine.Sessions;

    [DataContract]
    [KnownType(typeof(Commandlist))]
    [KnownType(typeof(Modulelist))]
    public class SessionData : ISessionData
    {
        [DataMember(Name = "Modulelist")]
        public IModulelist Modulelist { get; private set; }

        [DataMember(Name = "Commandlist")]
        public ICommandlist Commandlist { get; private set; }

        public SessionData()
        {
            this.Commandlist = new Commandlist();
            this.Modulelist  = new Modulelist(this.Commandlist);
        }

        #region Serialization

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (this.Commandlist == null)
            {
                this.Commandlist = new Commandlist();
            }

            if (this.Modulelist == null)
            {
                this.Modulelist = new Modulelist(this.Commandlist);
            }
        }

        #endregion Serialization

        public void Update()
        {
            this.Commandlist.Update();
            this.Modulelist .Update();
        }
    }
}