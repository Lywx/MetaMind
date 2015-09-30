// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Controls.Item
{
    public static class ViewItemStateExtension
    {
        public static bool Match(this ViewItemState state, IViewItem item)
        {
            return item[state]();
        }
    }
}