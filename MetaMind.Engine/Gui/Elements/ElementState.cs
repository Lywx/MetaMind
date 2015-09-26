namespace MetaMind.Engine.Gui.Elements
{
    public enum ElementState
    {
        Mouse_Is_Over,

        Mouse_Is_Left_Pressed,

        Mouse_Is_Left_Pressed_Out,

        Mouse_Is_Left_Released,

        Mouse_Is_Left_Double_Clicked,

        Mouse_Is_Right_Pressed,

        Mouse_Is_Right_Pressed_Out,

        Mouse_Is_Right_Released,

        Mouse_Is_Right_Double_Clicked,

        Element_Is_Active,

        // Whether frame position is being moved
        Element_Is_Moving,

        // Whether frame is prepare to be dragged
        Element_Is_Holding,

        // Whether frame is dragged by mouse
        Element_Is_Dragging,

        StateNum,
    }
}