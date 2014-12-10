// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemViewControl2D : ViewItemViewControl1D
    {
        public ViewItemViewControl2D(IViewItem item)
            : base(item)
        {
        }

        protected override void UpdateViewScroll()
        {
            base.UpdateViewScroll();

            this.ItemControl.Row    = this.ItemControl.View.Control.RowFrom(this.ItemControl.Id);
            this.ItemControl.Column = this.ItemControl.View.Control.ColumnFrom(this.ItemControl.Id);
        }
    }
}