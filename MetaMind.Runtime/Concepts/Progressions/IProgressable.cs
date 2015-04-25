// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProgressable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Concepts.Progressions
{
    public interface IProgressable
    {
        IProgressionData ProgressionData { get; set; }

        string ProgressionName { get; }
    }
}