namespace MetaMind.Engine.Guis.Elements.Regions
{
    public enum RegionState
    {
        Region_Mouse_Over,
        Region_Has_Focus,

        StateNum,
    }

    public static class RegionStateExt
    {
        public static void EnableStateIn(this RegionState state, bool[] states)
        {
            states[(int)state] = true;
        }

        public static void DisableStateIn(this RegionState state, bool[] states)
        {
            states[(int)state] = false;
        }

        public static bool IsStateEnabledIn(this RegionState state, bool[] states)
        {
            return states[(int)state];
        }
    }
}