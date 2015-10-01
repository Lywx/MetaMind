namespace MetaMind.Engine.Entities
{
    using System;

    public interface IMMUpdateable : IMMUpdateableOperations
    {
        bool Enabled { get; set; }

        int UpdateOrder { get; set; }

        event EventHandler<EventArgs> EnabledChanged;

        event EventHandler<EventArgs> UpdateOrderChanged;
    }
}