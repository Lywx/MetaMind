// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;

    public class PointView2DItemInteraction : PointViewHorizontalItemInteraction
    {
        private readonly PointView2DItemLogic itemLogic;

        public PointView2DItemInteraction(IViewItem item, IViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
            var itemLayer = this.ItemGetLayer<PointView2DItemLayer>();
            this.itemLogic = itemLayer.ItemLogic;

            var viewLayer = this.ViewGetLayer<PointView2DLayer>();
        }
    }
}