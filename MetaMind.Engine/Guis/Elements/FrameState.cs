namespace MetaMind.Engine.Guis.Elements
{
    public enum FrameState
    {
        Mouse_Over,

        Mouse_Left_Pressed,
        Mouse_Left_Released,

        Mouse_Left_Clicked,
        Mouse_Left_Clicked_Outside,
        Mouse_Left_Double_Clicked,

        Mouse_Left_Dragged_Out,

        Mouse_Right_Pressed,
        Mouse_Right_Released,

        Mouse_Right_Clicked,
        Mouse_Right_Clicked_Outside,
        Mouse_Right_Double_Clicked,

        Mouse_Right_Dragged_Out,

        Frame_Active,
        
        Frame_Initialized,

        // whether frame position is being moved
        Frame_Moved,

        // whether frame is prepare to be dragged
        Frame_Holding,

        // whether frame is dragged by mouse
        Frame_Dragging,

        StateNum,
    }
}