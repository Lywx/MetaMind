namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using System;

    public class ViewSettings : WidgetSettings, ICloneable
    {
        #region Control

        public bool MouseEnabled = true;

        public bool KeyboardEnabled = true;

        #endregion

        #region Properties

        public bool ReadOnly = false;

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}