namespace MetaMind.Engine
{
    using System;

    public interface IOuterUpdateable : IOuterUpdateableOperations
    {
        bool Enabled { get; }

        int UpdateOrder { get; }

        event EventHandler<EventArgs> EnabledChanged;

        event EventHandler<EventArgs> UpdateOrderChanged;
    }
}