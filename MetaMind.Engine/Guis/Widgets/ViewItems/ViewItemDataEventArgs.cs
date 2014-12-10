namespace MetaMind.Engine.Guis.Widgets.ViewItems
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