// --------------------------------------------------------------------------------------------------------------------
// <copyright file="viewLayout.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class ViewLayout : ViewComponent, IViewLayout
    {
        public ViewLayout(IView view) : base(view)
        {
        }

        public void Sort(Func<IViewItem, dynamic> key)
        {
            this.View.ItemsWrite = this.View.ItemsRead.OrderBy(key).ToList();
            this.View.ItemsWrite.ForEach(item => item.ItemLogic.ItemLayout.Id = this.View.ItemsRead.IndexOf(item));
        }
    }
}