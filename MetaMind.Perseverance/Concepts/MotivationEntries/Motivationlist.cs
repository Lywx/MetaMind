using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    public enum MotivationSpace
    {
        Past, Now, Future,
    }

    [DataContract]
    public class Motivationlist
    {
        public Motivationlist()
        {
            PastMotivations   = new List<MotivationEntry>();
            NowMotivations    = new List<MotivationEntry>();
            FutureMotivations = new List<MotivationEntry>();
        }

        [DataMember] public List<MotivationEntry> FutureMotivations { get; private set; }
        [DataMember] public List<MotivationEntry> NowMotivations    { get; private set; }
        [DataMember] public List<MotivationEntry> PastMotivations   { get; private set; }

        public MotivationEntry Create( MotivationSpace space )
        {
            var entry = new MotivationEntry();
            switch ( space )
            {
                case MotivationSpace.Past:
                {
                    PastMotivations.Add( entry );
                    break;
                }
                case MotivationSpace.Now:
                {
                    NowMotivations.Add( entry );
                    break;
                }
                case MotivationSpace.Future:
                {
                    FutureMotivations.Add( entry );
                    break;
                }
            }
            return entry;
        }

        public void Remove( MotivationEntry entry, MotivationSpace space )
        {
            switch ( space )
            {
                case MotivationSpace.Past:
                {
                    PastMotivations.Remove( entry );
                    break;
                }
                case MotivationSpace.Now:
                {
                    NowMotivations.Remove( entry );
                    break;
                }
                case MotivationSpace.Future:
                {
                    FutureMotivations.Remove( entry );
                    break;
                }
            }
        }
    }
}