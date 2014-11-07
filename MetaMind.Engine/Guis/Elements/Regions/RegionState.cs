namespace MetaMind.Engine.Guis.Elements.Regions
{
    public enum RegionState
    {
        // basic
        Region_Hightlighted,

        StateNum,
    }

    public static class RegionStateExt
    {
        public static void EnableStateIn( this RegionState state, bool[ ] states )
        {
            states[ ( int ) state ] = true;
        }

        public static void DisableStateIn( this RegionState state, bool[ ] states )
        {
            states[ ( int ) state ] = false;
        }

        public static bool IsStateEnabledIn( this RegionState state, bool[ ] states )
        {
            return states[ ( int ) state ];
        }
    }
}