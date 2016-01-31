namespace MetaMind.Engine.Core.Entity.Input
{
    using System;

    /// <summary>
    ///     Interface supporting basic mouse picking operation and selection.
    /// </summary>
    public interface IMMPickable : IMMPressable
    {
        #region Mouse Left Buttons

        event EventHandler<MMElementEventArgs> MouseDoubleClickLeft;

        #endregion Mouse Left Buttons

        #region Mouse Right Buttons

        event EventHandler<MMElementEventArgs> MouseDoubleClickRight;

        #endregion Mouse Right Buttons

        #region Mouse General

        event EventHandler<MMElementEventArgs> MouseDoubleClick;

        #endregion Mouse General
    }
}