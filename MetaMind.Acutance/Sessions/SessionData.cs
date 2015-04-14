namespace MetaMind.Acutance.Sessions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Concepts;

    [DataContract]
    [KnownType(typeof(Commandlist))]
    [KnownType(typeof(Modulelist))]
    public class SessionData
    {
        public Random Random { get; private set; }

        [DataMember(Name = "Modulelist")]
        public IModulelist Modulelist { get; private set; }

        [DataMember(Name = "Commandlist")]
        public ICommandlist Commandlist { get; private set; }

        public SessionData()
        {
            // FIXME: MOVE
            this.Random = new Random((int)DateTime.Now.Ticks);

            this.Commandlist = new Commandlist();
            this.Modulelist = new Modulelist(this.Commandlist);
        }

        #region Serialization

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (this.Random == null)
            {
                this.Random = new Random((int)DateTime.Now.Ticks);
            }

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
    }
}