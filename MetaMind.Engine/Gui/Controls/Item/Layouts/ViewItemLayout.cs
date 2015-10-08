// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemLayout.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item.Layouts
{
    using System;
    using Interactions;
    using Microsoft.Xna.Framework;
    using Views;

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
        }

        #region Layout

        public virtual int Id { get; set; }

        #endregion

        #region Logic

        private Func<bool> itemIsActive;

        public Func<bool> ItemIsActive
        {
            get { return this.itemIsActive; }
            set
            {
                this.itemIsActive = value;
                this.Item[ViewItemState.Item_Is_Active] = value;
            }
        }

        private void SetupLogic()
        {
            if (this.ItemIsActive == null)
            {
                this.ItemIsActive =
                    () => this.View[MMViewState.View_Is_Active]() &&
                          this.itemLayoutInteraction.ViewCanDisplay(this);
            }
        }

        #endregion

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            this.SetupLogic();
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.UpdateId();
        }

        protected void UpdateId()
        {
            this.Id = this.View.ItemsRead.IndexOf(this.Item);
        }

        #endregion
    }
}