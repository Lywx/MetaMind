namespace MetaMind.Engine.Gui.Controls.Item.Data
{
    using System;

    public class ViewItemDataEventArgs : EventArgs
    {
        public readonly string NewValue;

        public ViewItemDataEventArgs(string newValue)
        {
            this.NewValue = newValue;
        }
    }
}