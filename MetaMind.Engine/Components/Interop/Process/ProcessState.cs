// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessState.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Interop.Process
{
    public enum ProcessState
    {
        // Processes that are neither dade or alive
        Uninitilized, 

        Removed, 

        // Living Processes
        Running, 

        Paused, 

        // Dead Processes
        Succeeded, 

        Failed, 

        Aborted
    }
}