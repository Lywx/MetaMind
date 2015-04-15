namespace MetaMind.Engine.Guis.Elements
{
    public enum FrameEventType
    {
        //------------------------------------------------------------------
        // Basic
        Mouse_Enter,
        Mouse_Leave,

        //------------------------------------------------------------------
        // Left
        Mouse_Left_Pressed,
        Mouse_Left_Released,

        Mouse_Left_Clicked,
        Mouse_Left_Clicked_Outside,
        Mouse_Left_Double_Clicked,
        Mouse_Left_Double_Clicked_Outside,

        Mouse_Left_Dragged_Out,

        //------------------------------------------------------------------
        // Right
        Mouse_Right_Pressed,
        Mouse_Right_Released,

        Mouse_Right_Clicked,
        Mouse_Right_Clicked_Outside,
        Mouse_Right_Double_Clicked,
        Mouse_Right_Double_Clicked_Outside,

        Mouse_Right_Dragged_Out,

        //------------------------------------------------------------------
        // Change
        Frame_Moved,
        Frame_Dragged,
        Frame_Dropped,

        //------------------------------------------------------------------
        EventNum,
    }
}