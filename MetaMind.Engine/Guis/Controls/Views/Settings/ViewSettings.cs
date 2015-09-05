namespace MetaMind.Engine.Guis.Controls.Views.Settings
{
    using System;

    public class ViewSettings : ControlSettings, ICloneable
    {
        #region Control

        public bool MouseEnabled = true;

        public bool KeyboardEnabled = true;

        #endregion

        #region Properties

        public bool ReadOnly = true;

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}