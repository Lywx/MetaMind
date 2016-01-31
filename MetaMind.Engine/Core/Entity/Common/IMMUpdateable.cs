namespace MetaMind.Engine.Core.Entity.Common
{
    using System;

    public interface IMMUpdateable : IMMUpdateableOperations
    {
        bool EntityEnabled { get; set; }

        int EntityUpdateOrder { get; set; }

        event EventHandler<EventArgs> EntityEnabledChanged;

        event EventHandler<EventArgs> EntityUpdateOrderChanged;
    }
}