namespace MetaMind.Engine.Entities.Elements
{
    using System;

    /// <summary>
    ///     Interface supporting basic mouse picking operation and selection.
    /// </summary>
    public interface IMMInputPickable : IMMInputPressable
    {
        #region Mouse Left Buttons

        event EventHandler<MMInputElementDebugEventArgs> MouseDoubleClickLeft;

        #endregion Mouse Left Buttons

        #region Mouse Right Buttons

        event EventHandler<MMInputElementDebugEventArgs> MouseDoubleClickRight;

        #endregion Mouse Right Buttons

        #region Mouse General

        event EventHandler<MMInputElementDebugEventArgs> MouseDoubleClick;

        #endregion Mouse General
    }
}