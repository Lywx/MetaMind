namespace MetaMind.Engine.Guis.Elements
{
    public class FrameObject : EngineObject
    {
        private bool[] states;

        protected FrameObject()
        {
            this.states = new bool[(int)FrameState.StateNum];
        }

        public bool[] States { get { return this.states; } }

        #region Helper Methods

        public bool IsEnabled(FrameState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        public void Disable(FrameState state)
        {
            state.DisableStateIn(this.states);
        }

        protected void Enable(FrameState state)
        {
            state.EnableStateIn(this.states);
        }

        #endregion Helper Methods
    }
}