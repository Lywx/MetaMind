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

        MotivationCreateItem, 
        MotivationDeleteItem, 
        MotivationEditItem, 

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
        Awaken,
        Sleep,

        // General
        Enter, 
        Escape, 

        ActionNum,
    }
}