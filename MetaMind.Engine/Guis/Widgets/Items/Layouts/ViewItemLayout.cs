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
        private readonly IViewItemLayoutInteraction interaction;

        protected ViewItemLayout(IViewItem item, IViewItemLayoutInteraction interaction)
            : base(item)
        {
            if (interaction == null)
            {
                throw new ArgumentNullException("interaction");
            }

            this.interaction = interaction;

            this.Item[ItemState.Item_Is_Active] = this.ItemIsActive;
        }

        public Func<bool> ItemIsActive
        {
            get
            {
                return () => this.View[ViewState.View_Is_Active]() && this.interaction.ViewCanDisplay(this);
            }
        }

        public int Id { get; set; }

        #region Update

        public override void Update(GameTime time)
        {
            this.Id = this.View.Items.IndexOf(this.Item);
        }

        #endregion
    }
}