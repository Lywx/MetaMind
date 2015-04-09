namespace MetaMind.Perseverance.Concepts.Motivations
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class MotivationList : List<Motivation>
    {
    }
}