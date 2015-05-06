namespace MetaMind.Engine.Guis.Widgets.Items
{
    public enum ItemState
    {
        // whether is inside view region
        Item_Is_Active,

        // whether is accepting input
        Item_Is_Inputting,

        // whether is locking specific input
        Item_Is_Locking,

        // whether is visible
        Item_Is_Visible,

        // whether is under mouse cursor
        Item_Is_Mouse_Over,

        // whether is selected
        Item_Is_Selected,

        // whether is in pre-edit mode
        Item_Is_Pending,

        // whether is editing
        Item_Is_Editing,

        // whether is swapping with another item
        Item_Is_Swaping,

        // whether is dragging by mouse
        Item_Is_Dragging,

        //------------------------------------------------------------------
        StateNum,
    }
}