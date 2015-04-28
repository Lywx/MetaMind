namespace MetaMind.Engine.Guis.Elements
{
    public enum FrameState
    {
        Mouse_Is_Over,

        Mouse_Is_Left_Pressed,
        Mouse_Is_Left_Pressed_Outside,
        Mouse_Is_Left_Released,

        Mouse_Is_Left_Double_Clicked,

        Mouse_Is_Right_Pressed,
        Mouse_Is_Right_Pressed_Outside,
        Mouse_Is_Right_Released,

        Mouse_Is_Right_Double_Clicked,

        Frame_Is_Active,

        // Whether frame position is being moved
        Frame_Is_Moving,

        // Whether frame is prepare to be dragged
        Frame_Is_Holding,

        // Whether frame is dragged by mouse
        Frame_Is_Dragging,

        StateNum,
    }
}