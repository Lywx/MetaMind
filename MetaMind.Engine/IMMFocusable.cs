// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine
{
    /// <summary>
    /// Interface for nodes that can be focused. 
    /// </summary>
    public interface IMMFocusable
    {
        bool CanFocus { get; }

        bool HasFocus { get; set; }
    }
}