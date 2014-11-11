using MetaMind.Engine.Guis.Elements.Frames;
using System;

namespace MetaMind.Engine.Guis.Elements.Abstract
{
    /// <summary>
    /// Interface supporting basic mouse picking operation and selection.
    /// </summary>
    public interface IPickable
    {
        event EventHandler<FrameEventArgs> MouseLeftClicked;
        event EventHandler<FrameEventArgs> MouseLeftClickedOutside;
        event EventHandler<FrameEventArgs> MouseLeftDoubleClicked;

        event EventHandler<FrameEventArgs> MouseRightClicked;
        event EventHandler<FrameEventArgs> MouseRightClickedOutside;
        event EventHandler<FrameEventArgs> MouseRightDoubleClicked;
    }
}