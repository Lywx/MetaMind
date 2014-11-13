namespace MetaMind.Engine.Guis.Elements.Views
{
    public enum ViewState
    {
        // ReSharper disable InconsistentNaming
        View_Active,

        View_Has_Focus,
        View_Has_Selection,

        Item_Editting,
        Item_Dragging,

        // ReSharper restore InconsistentNaming
        StateNum,
    }

    public static class ViewStateExt
    {
        public static void EnableStateIn( this ViewState state, bool[ ] states )
        {
            states[ ( int ) state ] = true;
        }

        public static void DisableStateIn( this ViewState state, bool[ ] states )
        {
            states[ ( int ) state ] = false;
        }

        public static bool IsStateEnabledIn( this ViewState state, bool[ ] states )
        {
            return states[ ( int ) state ];
        }
    }
}