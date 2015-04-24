// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProgressionData.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Concepts.Progressions
{
    using System.Runtime.Serialization;

    public interface IProgressionData
    {
        [DataMember]
        int Done { get; set; }

        [DataMember]
        int Load { get; set; }
    }
}