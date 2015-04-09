namespace MetaMind.Engine.Guis.Widgets.Regions
{
    public class RegionObject : GameEngineAccess
    {
        private bool[] states;

        protected RegionObject()
        {
            this.states = new bool[(int)RegionState.StateNum];
        }

        public bool[] States { get { return this.states; } }

        #region States

        public bool IsEnabled(RegionState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        public void Disable(RegionState state)
        {
            state.DisableStateIn(this.states);
        }

        public void Enable(RegionState state)
        {
            state.EnableStateIn(this.states);
        }

        #endregion States
    }
}