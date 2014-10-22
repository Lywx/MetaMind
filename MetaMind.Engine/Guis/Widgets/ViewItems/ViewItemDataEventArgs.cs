using System;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemDataEventArgs : EventArgs
    {
        public readonly string NewValue;

        public ViewItemDataEventArgs(string newValue)
        {
            NewValue = newValue;
        }
    }
}