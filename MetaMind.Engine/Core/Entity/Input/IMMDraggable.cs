namespace MetaMind.Engine.Core.Entity.Input
{
    using System;

    public interface IMMDraggable
    {
        bool Movable { get; }

        event EventHandler<MMElementEventArgs> MouseDrag;

        event EventHandler<MMElementEventArgs> MouseDrop;
    }
}