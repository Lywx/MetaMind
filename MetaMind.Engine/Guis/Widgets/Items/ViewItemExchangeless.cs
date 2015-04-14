﻿namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using Microsoft.Xna.Framework;

    public class ViewItemExchangeless : ItemEntity, IViewItem
    {
        public ViewItemExchangeless(dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(itemSettings)
        {
            this.View         = view;
            this.ViewSettings = viewSettings;

            this.ItemData     = itemFactory.CreateData(this);
            this.ItemControl  = itemFactory.CreateControl(this);
            this.ItemGraphics = itemFactory.CreateGraphics(this);
        }

        public ViewItemExchangeless(dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory, dynamic itemData)
            : base(itemSettings)
        {
            this.View         = view;
            this.ViewSettings = viewSettings;

            this.ItemData     = itemData;
            this.ItemControl  = itemFactory.CreateControl(this);
            this.ItemGraphics = itemFactory.CreateGraphics(this);
        }

        ~ViewItemExchangeless()
        {
            this.Dispose();
        }

        public dynamic ItemControl { get; set; }

        public dynamic ItemData { get; set; }

        public IItemGraphics ItemGraphics { get; set; }

        public dynamic View { get; protected set; }

        public dynamic ViewControl
        {
            get { return this.View.Control; }
        }

        public dynamic ViewSettings { get; protected set; }

        #region Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.ItemGraphics.Draw(gameGraphics, gameTime, alpha);
        }

        #endregion

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.ItemControl .Update(gameInput, gameTime);
            this.ItemGraphics.UpdateInput(gameInput, gameTime);
        }

        public override void UpdateView(GameTime gameTime)
        {
            this.ItemControl.UpdateView(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.ItemControl .Update(gameTime);
            this.ItemGraphics.Update(gameTime);
        }

        public override void Dispose()
        {
            try
            {
                // don't set item control to null
                // because the reference is used in the loop
                // that cannot dispose itself
                this.ItemControl.Dispose();

                this.ItemGraphics = null;
            }
            finally
            {
                base.Dispose();
            }
        }
    }
}