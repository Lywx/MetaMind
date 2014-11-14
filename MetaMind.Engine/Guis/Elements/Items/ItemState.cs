namespace MetaMind.Engine.Guis.Elements.Items
{
    public enum ItemState
    {
        // whether is inside view region
        Item_Active,

        // whether is visible
        Item_Visible,

        // whether is under mouse cursor
        Item_Mouse_Over,

        // whether is selected
        Item_Selected,

        // whether is in pre-edit mode
        Item_Pending,

        // whether is editing
        Item_Editing,

        // whether is swapping with another item
        Item_Swaping,

        // whether is exchanging to another view
        Item_Exchanging,

        // whether is dragging by mouse
        Item_Dragging,

        // whether undergoes group dragging
        Item_Multiple_Dragging,

        //------------------------------------------------------------------
        StateNum,
    }

    public static class ItemStateExt
    {
        public static void EnableStateIn(this ItemState state, bool[] states)
        {
            states[(int)state] = true;
        }

        public static void DisableStateIn(this ItemState state, bool[] states)
        {
            states[(int)state] = false;
        }

        public static bool IsStateEnabledIn(this ItemState state, bool[] states)
        {
            return states[(int)state];
        }
    }
}