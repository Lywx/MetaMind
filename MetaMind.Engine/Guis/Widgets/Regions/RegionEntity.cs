namespace MetaMind.Engine.Guis.Widgets.Regions
{
    public abstract class RegionEntity : InputableGameEntity
    {
        #region Constructors

        protected RegionEntity()
        {
            this.states = new bool[(int)RegionState.StateNum];
        }

        #endregion

        #region States

        private bool[] states;

        public bool[] States { get { return this.states; } }

        public void Disable(RegionState state)
        {
            state.DisableStateIn(this.states);
        }

        public void Enable(RegionState state)
        {
            state.EnableStateIn(this.states);
        }

        public bool IsEnabled(RegionState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        #endregion States
    }
}