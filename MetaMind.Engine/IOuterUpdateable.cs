namespace MetaMind.Engine
{
    using System;

    public interface IOuterUpdateable : IOuterUpdateableOperations
    {
        bool Enabled { get; set; }

        int UpdateOrder { get; set; }

        event EventHandler<EventArgs> EnabledChanged;

        event EventHandler<EventArgs> UpdateOrderChanged;
    }
}