// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;

    public class PointView2DItemViewControl : ViewItemViewControl
    {
        public PointView2DItemViewControl(IViewItem item)
            : base(item)
        {
        }

        public override Func<bool> ItemIsActive
        {
            get
            {
                
            }
        }

        protected override void UpdateViewItem()
        {
            base.UpdateViewItem();

            var itemExtension = this.ItemExtension.Get<PointView2DItemExtension>();
            var viewExtension = this.ViewExtension.Get<PointView2DExtension>();
            itemExtension.ItemLogic.Row    = viewExtension.ViewLogic.RowFrom(this.ItemLogic.Id);
            itemExtension.ItemLogic.Column = viewExtension.ViewLogic.ColumnFrom(this.ItemLogic.Id);
        }
    }
}