// --------------------------------------------------------------------------------------------------------------------
// <copyright file="viewLayout.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    using System;
    using Item;

    public class ViewLayout : MMViewControlComponent, IMMViewLayout
    {
        public ViewLayout(IMMView view) : base(view)
        {
        }

        public void Sort(Func<IMMViewItem, dynamic> key)
        {
            this.View.ItemsWrite = this.View.ItemsRead.OrderBy(key).ToList();

            // Re-label items' id based on new order
            this.View.ItemsWrite.ForEach(item => item.ItemLogic.ItemLayout.Id = this.View.ItemsWrite.IndexOf(item));
        }
    }
}