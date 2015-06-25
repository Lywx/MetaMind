namespace MetaMind.Engine
{
    using System;

    public interface IDrawable : IDrawableOperations 
    {
        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }
    }
}