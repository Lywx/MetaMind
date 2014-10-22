namespace MetaMind.Engine.Guis.Elements.Frames
{
    public class FrameObject : EngineObject
    {
        private bool[] states;
        public bool[ ] States { get { return states; } }

        protected FrameObject()
        {
            states = new bool[ ( int ) FrameState.StateNum ];
        }

        #region Helper Methods

        protected void Disable( FrameState state )
        {
            state.DisableStateIn( states );
        }

        protected void Enable( FrameState state )
        {
            state.EnableStateIn( states );
        }

        public bool IsEnabled( FrameState state )
        {
            return state.IsStateEnabledIn( states );
        }

        #endregion Helper Methods
    }
}