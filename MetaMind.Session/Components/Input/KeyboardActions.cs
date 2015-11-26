namespace MetaMind.Session.Components.Input
{
    /// <summary>
    /// The actions that are possible within the game.
    /// </summary>
    public enum KeyboardActions
    {
        // ---------------
        // Cursor Movement
        // ---------------
        Up, 
        Down, 
        Left, 
        Right, 

        FastUp, 
        FastDown, 
        FastLeft, 
        FastRight, 

        // ---------------
        // List Management
        // ---------------
        CommonCreateItem,
        CommonDeleteItem,
        CommonEditItem,

        // -------------
        // Consciousness
        // -------------
        ConsciousnessAwaken,
        ConsciousnessSleep,

        // ---------------
        // Synchronization
        // ---------------
        SynchronizationPause,
        SynchronizationReverse,

        // Fsi Session
        SessionRerun,

        // General
        Enter, 
        Escape, 
    }
}