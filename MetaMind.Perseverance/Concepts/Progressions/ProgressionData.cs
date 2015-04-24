// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressionData.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Concepts.Progressions
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ProgressionData : IProgressionData
    {
        public ProgressionData()
        {
            this.Load = 0;
            this.Done = 0;
        }

        [DataMember]
        public int Done { get; set; }

        [DataMember]
        public int Load { get; set; }
    }
}