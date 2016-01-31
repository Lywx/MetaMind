namespace MetaMind.Engine.Core.Entity.Input
{
    public enum MMElementState
    {
        /// <summary>
        /// Element States.
        /// </summary>

        // Whether element is being updated
        Element_Is_Active,

        // Whether element position is being moved
        Element_Is_Moving,

        // Whether element is prepare to be dragged
        Element_Is_Holding,

        // Whether element is dragged by mouse
        Element_Is_Dragging,

        /// <summary>
        /// Input States.
        /// </summary>

        Mouse_Is_Over,

        Mouse_Is_Left_Pressed,

        Mouse_Is_Left_Pressed_Out,

        Mouse_Is_Left_Released,

        Mouse_Is_Left_Double_Clicked,

        Mouse_Is_Right_Pressed,

        Mouse_Is_Right_Pressed_Out,

        Mouse_Is_Right_Released,

        Mouse_Is_Right_Double_Clicked,

        /// 

        StateNum,
    }
}