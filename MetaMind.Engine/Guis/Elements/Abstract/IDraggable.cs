using System;
using MetaMind.Engine.Guis.Elements.Frames;

namespace MetaMind.Engine.Guis.Elements.Abstract
{
    public interface IDraggable
    {
        event EventHandler<FrameEventArgs> MouseDragged;
        event EventHandler<FrameEventArgs> MouseDropped;
    }
}