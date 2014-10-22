namespace MetaMind.Engine.Guis.Elements.Frames
{
    public enum FrameState
    {
        // basic
        Mouse_Over,

        // left
        Mouse_Left_Pressed,
        Mouse_Left_Released,

        Mouse_Left_Clicked,
        Mouse_Left_Clicked_Outside,
        Mouse_Left_Double_Clicked,

        Mouse_Left_Dragged_Out,

        // right 6
        Mouse_Right_Pressed,
        Mouse_Right_Released,

        Mouse_Right_Clicked,
        Mouse_Right_Clicked_Outside,
        Mouse_Right_Double_Clicked,

        Mouse_Right_Dragged_Out,

        // change
        Frame_Initialized,
        Frame_Active,
        Frame_Moved,
        Frame_Holding, // prepare to drag
        Frame_Dragging,

        StateNum,
    }

    public static class FrameStateExtensions
    {
        public static void EnableStateIn( this FrameState state, bool[ ] states )
        {
            states[ ( int ) state ] = true;
        }

        public static void DisableStateIn( this FrameState state, bool[ ] states )
        {
            states[ ( int ) state ] = false;
        }

        public static bool IsStateEnabledIn( this FrameState state, bool[ ] states )
        {
            return states[ ( int ) state ];
        }
    }
}