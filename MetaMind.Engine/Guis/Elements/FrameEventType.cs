namespace MetaMind.Engine.Guis.Elements
{
    public enum FrameEventType
    {
        // Basic
        Mouse_Enter,
        Mouse_Leave,

        // Left
        Mouse_Left_Pressed,
        Mouse_Left_Pressed_Outside,

        Mouse_Left_Released,

        Mouse_Left_Double_Clicked,

        // Right
        Mouse_Right_Pressed,
        Mouse_Right_Pressed_Outside,

        Mouse_Right_Released,
        
        Mouse_Right_Double_Clicked,

        // Change
        Frame_Moved,
        Frame_Dragged,
        Frame_Dropped,

        EventNum,
    }
}