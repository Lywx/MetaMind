namespace MetaMind.Engine.Guis.Elements
{
    using System;

    public interface IDraggable
    {
        event EventHandler<FrameEventArgs> MouseDragged;
        event EventHandler<FrameEventArgs> MouseDropped;
    }
}