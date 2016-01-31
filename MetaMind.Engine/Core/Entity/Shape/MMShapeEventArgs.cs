namespace MetaMind.Engine.Core.Entity.Shape
{
    using System;

    public class MMShapeEventArgs : EventArgs
    {
        public MMShapeEventArgs(MMShapeEvent @event)
        {
            this.Event = @event;
        }

        public MMShapeEvent Event { get; }
    }
}