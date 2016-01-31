namespace MetaMind.Engine.Core.Entity.Common
{
    using System;

    public interface __IMMDrawableBase : IComparable<IMMDrawable> 
    {
    }

    public interface IMMDrawable : __IMMDrawableBase, IMMDrawableOperations
    {
        event EventHandler<EventArgs> EntityDrawOrderChanged;

        event EventHandler<EventArgs> EntityVisibleChanged;

        int EntityDrawOrder { get; }

        bool EntityVisible { get; }
    }
}