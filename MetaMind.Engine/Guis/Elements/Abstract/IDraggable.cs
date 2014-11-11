using MetaMind.Engine.Guis.Elements.Frames;
using System;

namespace MetaMind.Engine.Guis.Elements.Abstract
{
    public interface IDraggable
    {
        event EventHandler<FrameEventArgs> MouseDragged;
        event EventHandler<FrameEventArgs> MouseDropped;
    }
}