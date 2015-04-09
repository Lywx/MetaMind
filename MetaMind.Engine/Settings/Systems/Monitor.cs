// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Graphics.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Settings.Systems
{
    using System.Linq;
    using System.Windows.Forms;

    public static class Monitor
    {
        public static Screen Screen
        {
            get
            {
                return Screen.AllScreens.First(e => e.Primary);
            }
        }
    }
}