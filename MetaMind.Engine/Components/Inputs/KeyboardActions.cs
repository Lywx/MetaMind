namespace MetaMind.Engine.Components.Inputs
{
    /// <summary>
    /// The actions that are possible within the game.
    /// </summary>
    public enum KeyboardActions
    {
        // Cursor Movement
        Up, 
        Down, 
        Left, 
        Right, 

        FastUp, 
        FastDown, 
        FastLeft, 
        FastRight, 

        // List Management
        CommonCreateItem,
        CommonDeleteItem,
        CommonEditItem,

        KnowledgeLoadBuffer,

        ModuleClearItem,
        ModuleDeleteItem,
        ModuleOpenItem,
        ModuleResetItem,
        ModuleSortItem,

        CommandClearItem,
        CommandDeleteItem,
        CommandOpenItem,
        CommandSortItem,

        // Consciousness
        ConsciousnessAwaken,
        ConsciousnessSleep,

        // Synchronization
        SynchronizationPause,
        SynchronizationReverse,

        // Fsi Session
        SessionRerun,

        // General
        Enter, 
        Escape, 

        ActionNum,
    }
}