using System;
using MetaMind.Engine.Guis.Elements.Frames;

namespace MetaMind.Engine.Guis.Elements.Abstract
{
    public interface IPressable
    {
        event EventHandler<FrameEventArgs> MouseEnter;
        event EventHandler<FrameEventArgs> MouseLeave;

        event EventHandler<FrameEventArgs> MouseLeftPressed;
        event EventHandler<FrameEventArgs> MouseLeftReleased;
        event EventHandler<FrameEventArgs> MouseLeftDraggedOutside;

        event EventHandler<FrameEventArgs> MouseRightPressed;
        event EventHandler<FrameEventArgs> MouseRightReleased;
        event EventHandler<FrameEventArgs> MouseRightDraggedOutside;

        event EventHandler<FrameEventArgs> FrameMoved;
    }
}