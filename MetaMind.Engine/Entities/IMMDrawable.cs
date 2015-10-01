namespace MetaMind.Engine.Entities
{
    using System;

    public interface IMMDrawable : IMMDrawOperations 
    {
        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }
    }
}