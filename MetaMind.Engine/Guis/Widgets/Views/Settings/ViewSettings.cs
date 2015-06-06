namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using System;

    public class ViewSettings : WidgetSettings, ICloneable
    {
        public bool MouseEnabled     = true;

        public bool KeyboardEnabled  = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}