using System;

namespace MetaMind.Engine.Components.Inputs
{
    /// <summary>
    /// Mouse Key Flags from WinUser.h for mouse related WM messages.
    /// </summary>
    [Flags]
    public enum MouseKeys
    {
        LButton = 0x01,
        RButton = 0x02,
        Shift = 0x04,
        Control = 0x08,
        MButton = 0x10,
        XButton1 = 0x20,
        XButton2 = 0x40
    }
}