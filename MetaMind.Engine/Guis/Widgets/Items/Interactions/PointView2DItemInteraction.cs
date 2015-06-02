// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using Layers;
    using Layouts;
    using Views.Layers;

    public class PointView2DItemInteraction : PointViewHorizontalItemInteraction
    {
        private readonly PointView2DItemLayer itemLayer;
        private readonly PointView2DLayer viewLayer;

        public PointView2DItemInteraction(IViewItem item, IViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
            this.itemLayer = this.ItemGetLayer<PointView2DItemLayer>();
            this.viewLayer = this.ViewGetLayer<PointView2DLayer>();
        }
    }
}