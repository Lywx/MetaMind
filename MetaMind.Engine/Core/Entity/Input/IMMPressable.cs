namespace MetaMind.Engine.Core.Entity.Input
{
    using System;
    using Entity.Common;

    public interface IMMPressable : IMMInputtable
    {
        #region Mouse General

        event EventHandler<MMElementEventArgs> MouseEnter;

        event EventHandler<MMElementEventArgs> MouseLeave;

        event EventHandler<MMElementEventArgs> MousePress;

        event EventHandler<MMElementEventArgs> MousePressOut;

        event EventHandler<MMElementEventArgs> MouseUp;

        event EventHandler<MMElementEventArgs> MouseUpOut;

        #endregion Mouse General

        #region Mouse Left Buttons

        event EventHandler<MMElementEventArgs> MousePressLeft;

        event EventHandler<MMElementEventArgs> MousePressOutLeft;

        event EventHandler<MMElementEventArgs> MouseUpLeft;

        event EventHandler<MMElementEventArgs> MouseUpOutLeft;

        #endregion Mouse Left Buttons

        #region Mouse Right Buttons

        event EventHandler<MMElementEventArgs> MousePressRight;

        event EventHandler<MMElementEventArgs> MousePressOutRight;

        event EventHandler<MMElementEventArgs> MouseUpRight;

        event EventHandler<MMElementEventArgs> MouseUpOutRight;

        #endregion Mouse Right Buttons
    }
}