namespace MetaMind.Engine.Guis.Elements.Regions
{
    public class RegionObject : EngineObject
    {
        private bool[] states;

        protected RegionObject()
        {
            states = new bool[ ( int ) RegionState.StateNum ];
        }

        public bool[ ] States { get { return states; } }

        #region States

        public bool IsEnabled( RegionState state )
        {
            return state.IsStateEnabledIn( states );
        }

        public void Disable( RegionState state )
        {
            state.DisableStateIn( states );
        }

        public void Enable( RegionState state )
        {
            state.EnableStateIn( states );
        }

        #endregion States
    }
}