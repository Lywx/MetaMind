// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.ItemView
{
    public class ViewItemView2DControl : ViewItemViewControl
    {
        public ViewItemView2DControl(IViewItem item)
            : base(item)
        {
        }

        protected override void UpdateViewScroll()
        {
            base.UpdateViewScroll();

            this.ItemLogic.Row    = this.ItemLogic.View.Control.RowFrom(this.ItemLogic.Id);
            this.ItemLogic.Column = this.ItemLogic.View.Control.ColumnFrom(this.ItemLogic.Id);
        }
    }
}