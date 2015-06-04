// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemLayout.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class ViewItemLayout : ViewItemComponent, IViewItemLayout
    {
        private readonly IViewItemLayoutInteraction itemLayoutInteraction;

        protected ViewItemLayout(IViewItem item, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item)
        {
            if (itemLayoutInteraction == null)
            {
                throw new ArgumentNullException("itemLayoutInteraction");
            }

            this.itemLayoutInteraction = itemLayoutInteraction;

            this.Item[ItemState.Item_Is_Active] = this.ItemIsActive;
        }

        public Func<bool> ItemIsActive
        {
            get
            {
                return () => this.View[ViewState.View_Is_Active]() && this.itemLayoutInteraction.ViewCanDisplay(this);
            }
        }

        public virtual int Id { get; set; }

        #region Update

        public override void Update(GameTime time)
        {
            this.UpdateId();
        }

        protected void UpdateId()
        {
            this.Id = this.View.ItemsRead.IndexOf(this.Item);
        }

        #endregion
    }
}