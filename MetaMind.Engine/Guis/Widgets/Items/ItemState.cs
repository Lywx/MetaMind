namespace MetaMind.Engine.Guis.Widgets.Items
{
    public enum ItemState
    {
        //------------------------------------------------------------------
        // whether is inside view region
        Item_Active,
        Item_Visible,

        //------------------------------------------------------------------
        Item_Mouse_Over,

        Item_Selected,
        Item_Pending,
        Item_Editing,

        Item_Swaping,

        Item_Dragging,
        Item_Multiple_Dragging,

        //------------------------------------------------------------------
        StateNum,
    }

    public static class ItemStateExt
    {
        public static void EnableStateIn( this ItemState state, bool[ ] states )
        {
            states[ ( int ) state ] = true;
        }

        public static void DisableStateIn( this ItemState state, bool[ ] states )
        {
            states[ ( int ) state ] = false;
        }

        public static bool IsStateEnabledIn( this ItemState state, bool[ ] states )
        {
            return states[ ( int ) state ];
        }
    }
}