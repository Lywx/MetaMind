namespace MetaMind.Engine.Entities.Elements
{
    using System;

    public interface IMMInputDraggable
    {
        bool Movable { get; }

        event EventHandler<MMInputElementDebugEventArgs> MouseDrag;

        event EventHandler<MMInputElementDebugEventArgs> MouseDrop;
    }
}