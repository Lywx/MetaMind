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
        MotivationCreateItem, 
        MotivationDeleteItem, 
        MotivationEditItem, 

        TaskCreateItem, 
        TaskDeleteItem, 
        TaskEditItem, 

        TraceCreateItem, 
        TraceDeleteItem, 
        TraceEditItem, 
        TraceClearItem, 

        KnowledgeEditItem,
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

        // Synchronization
        ForceAwake,
        ForceFlip,
        ForceReset,
        ForceReverse,

        // General
        Enter, 
        Escape, 

        ActionNum,
    }
}