namespace MetaMind.Engine.Core.Entity.Input
{
    public enum MMElementEvent
    {
        /// <summary>
        /// Input State.
        /// </summary>

        // Basic
        Mouse_Enter,

        Mouse_Leave,

        // Left
        Mouse_Press_Left,

        Mouse_Press_Out_Left,

        Mouse_Up_Left,

        Mouse_Up_Out_Left,

        Mouse_Double_Click_Left,

        // Right
        Mouse_Press_Right,

        Mouse_Pressed_Out_Right,

        Mouse_Up_Right,

        Mouse_Up_Out_Right,

        Mouse_Double_Click_Right,

        /// <summary>
        /// Element State.
        /// </summary>

        Element_Drag,

        Element_Drop,

        /// 

        EventNum,
    }
}
