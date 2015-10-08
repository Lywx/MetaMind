namespace MetaMind.Engine.Entities
{
    using System;
    using System.Collections.Generic;

    public interface IMMDrawable : IMMDrawOperations, IComparer<IMMDrawable>, IComparable<IMMDrawable>
    {
        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }
    }
}