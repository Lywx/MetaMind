namespace MetaMind.Engine.Guis.Elements
{
    public static class FrameStateExt
    {
        public static void EnableStateIn(this FrameState state, bool[] states)
        {
            states[(int)state] = true;
        }

        public static void DisableStateIn(this FrameState state, bool[] states)
        {
            states[(int)state] = false;
        }

        public static bool IsStateEnabledIn(this FrameState state, bool[] states)
        {
            return states[(int)state];
        }
    }
}