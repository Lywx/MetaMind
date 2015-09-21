// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessState.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.Interop.Process
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