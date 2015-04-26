namespace MetaMind.Engine.Guis.Widgets.Views
{
    public enum ViewState
    {
        View_Active,

        View_Has_Focus,
        View_Has_Selection,

        View_Editting,

        StateNum,
    }

    public static class ViewStateExt
    {
        public static void EnableStateIn(this ViewState state, bool[] states)
        {
            states[(int)state] = true;
        }

        public static void DisableStateIn(this ViewState state, bool[] states)
        {
            states[(int)state] = false;
        }

        public static bool IsStateEnabledIn(this ViewState state, bool[] states)
        {
            return states[(int)state];
        }
    }
}