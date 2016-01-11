namespace MetaMind.Engine.Entities.Shapes
{
    using System;

    public class MMShapeDebugEventArgs : EventArgs
    {
        public MMShapeDebugEventArgs(MMShapeDebugEvent debugEvent)
        {
            this.ShapeEvent = debugEvent;
        }

        public MMShapeDebugEvent ShapeEvent { get; }
    }
}