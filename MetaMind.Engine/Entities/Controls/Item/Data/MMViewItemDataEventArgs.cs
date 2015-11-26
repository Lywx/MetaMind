namespace MetaMind.Engine.Entities.Controls.Item.Data
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