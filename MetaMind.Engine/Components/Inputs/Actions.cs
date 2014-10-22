namespace MetaMind.Engine.Components.Inputs
{
    /// <summary>
    /// The actions that are possible within the game.
    /// </summary>
    public enum Actions
    {
        // cursor movement
        Up,
        Down,
        Left,
        Right,

        // tile management
        NewItem, // + ctrl
        NewChildItem, // + ctrl
        DeleteItem, // + ctrl

        // tile edit
        EditTileName,

        // concept 1
        PullItem,
        StretchItem,
        // concept 2
        FinishItem, // + ctrl

        // basic command
        LeftControl,
        Enter,
        Esc,

        ActionNum
    }
}