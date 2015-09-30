namespace MetaMind.Engine
{
    using System;

    public interface IMMDrawable : IMMDrawableOperations 
    {
        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }
    }
}