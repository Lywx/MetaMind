namespace MetaMind.Engine.Core.Entity.Control.Item.Data
{
    using System;

    public class MMViewItemDataEventArgs : EventArgs
    {
        public readonly string NewValue;

        public MMViewItemDataEventArgs(string newValue)
        {
            this.NewValue = newValue;
        }
    }
}