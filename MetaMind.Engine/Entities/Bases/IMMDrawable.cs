namespace MetaMind.Engine.Entities.Bases
{
    using System;
    using System.Collections.Generic;

    public interface IMMDrawableBase : IComparer<IMMDrawable>, IComparable<IMMDrawable> 
    {
    }

    public interface IMMDrawbleOperations : IMMDrawOperations 
    {
    }

    public interface IMMDrawable : IMMDrawableBase, IMMDrawbleOperations
    {
        event EventHandler<EventArgs> EntityDrawOrderChanged;

        event EventHandler<EventArgs> EntityVisibleChanged;

        int EntityDrawOrder { get; }

        bool EntityVisible { get; }
    }
}