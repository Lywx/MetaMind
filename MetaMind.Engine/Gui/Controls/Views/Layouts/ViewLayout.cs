// --------------------------------------------------------------------------------------------------------------------
// <copyright file="viewLayout.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Layouts
{
    using System;
    using System.Linq;
    using Item;

    public class ViewLayout : MMViewControlComponent, IViewLayout
    {
        public ViewLayout(IMMViewNode view) : base(view)
        {
        }

        public void Sort(Func<IViewItem, dynamic> key)
        {
            this.View.ItemsWrite = this.View.ItemsRead.OrderBy(key).ToList();

            // Re-label items' id based on new order
            this.View.ItemsWrite.ForEach(item => item.ItemLogic.ItemLayout.Id = this.View.ItemsWrite.IndexOf(item));
        }
    }
}