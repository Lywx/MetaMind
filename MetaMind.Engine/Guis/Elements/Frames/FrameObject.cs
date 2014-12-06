namespace MetaMind.Engine.Guis.Elements.Frames
{
    public class FrameObject : EngineObject
    {
        private bool[] states;

        protected FrameObject()
        {
            states = new bool[(int)FrameState.StateNum];
        }

        public bool[] States { get { return states; } }

        #region Helper Methods

        public bool IsEnabled(FrameState state)
        {
            return state.IsStateEnabledIn(states);
        }

        public void Disable(FrameState state)
        {
            state.DisableStateIn(states);
        }

        protected void Enable(FrameState state)
        {
            state.EnableStateIn(states);
        }

        #endregion Helper Methods
    }
}