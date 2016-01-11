namespace MetaMind.Engine.Entities.Elements
{
    using System;
    using Bases;

    public interface IMMInputPressable : IMMInputable
    {
        #region Mouse General

        event EventHandler<MMInputElementDebugEventArgs> MouseEnter;

        event EventHandler<MMInputElementDebugEventArgs> MouseLeave;

        event EventHandler<MMInputElementDebugEventArgs> MousePress;

        event EventHandler<MMInputElementDebugEventArgs> MousePressOut;

        event EventHandler<MMInputElementDebugEventArgs> MouseUp;

        event EventHandler<MMInputElementDebugEventArgs> MouseUpOut;

        #endregion Mouse General

        #region Mouse Left Buttons

        event EventHandler<MMInputElementDebugEventArgs> MousePressLeft;

        event EventHandler<MMInputElementDebugEventArgs> MousePressOutLeft;

        event EventHandler<MMInputElementDebugEventArgs> MouseUpLeft;

        event EventHandler<MMInputElementDebugEventArgs> MouseUpOutLeft;

        #endregion Mouse Left Buttons

        #region Mouse Right Buttons

        event EventHandler<MMInputElementDebugEventArgs> MousePressRight;

        event EventHandler<MMInputElementDebugEventArgs> MousePressOutRight;

        event EventHandler<MMInputElementDebugEventArgs> MouseUpRight;

        event EventHandler<MMInputElementDebugEventArgs> MouseUpOutRight;

        #endregion Mouse Right Buttons
    }
}