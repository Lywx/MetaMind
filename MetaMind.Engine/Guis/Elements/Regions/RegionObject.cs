namespace MetaMind.Engine.Guis.Elements.Regions
{
    public class RegionObject : EngineObject
    {
        private bool[] states;
        public bool[ ] States { get { return states; } }

        protected RegionObject()
        {
            states = new bool[ ( int ) RegionState.StateNum ];
        }

        #region Helper Methods

        protected void Disable( RegionState state )
        {
            state.DisableStateIn( states );
        }

        protected void Enable( RegionState state )
        {
            state.EnableStateIn( states );
        }

        public bool IsEnabled( RegionState state )
        {
            return state.IsStateEnabledIn( states );
        }

        #endregion Helper Methods
    }
}