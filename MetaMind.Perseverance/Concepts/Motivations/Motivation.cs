namespace MetaMind.Perseverance.Concepts.Motivations
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Perseverance.Concepts.Tasks;
    using MetaMind.Perseverance.Guis.Modules;

    [DataContract]
    public class Motivation : IProgressable
    {
        [DataMember]
        public string Name = string.Empty;

        [DataMember]
        public List<Task> Tasks = new List<Task>();

        public Motivation()
        {
        }

        //public void SwapWithInSpace(MotivationSpace space, Motivation target)
        //{
        //    var source = MotivationModuleSettings.GetMotivationSource(space);
        //    if (source != null && 
        //        source.Contains(this) && 
        //        source.Contains(target))
        //    {
        //        var thisIndex   = source.IndexOf(this);
        //        var targetIndex = source.IndexOf(target);

        //        source[thisIndex]   = target;
        //        source[targetIndex] = this;
        //    }
        //}

        #region Progression Data

        public ProgressionData ProgressionData { get; set; }

        public string ProgressionName
        {
            get { return this.Name; }
        }

        #endregion
    }
}