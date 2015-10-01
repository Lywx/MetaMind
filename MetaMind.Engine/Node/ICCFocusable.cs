// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node
{
    /// <summary>
    /// Interface for nodes that can be focused. 
    /// </summary>
    public interface ICCFocusable
    {
        bool CanReceiveFocus { get; }

        bool HasFocus { get; set; }
    }
}