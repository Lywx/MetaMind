namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IViewItem : IItemObject, IDisposable
    {
        dynamic ItemControl { get; set; }

        dynamic ItemData { get; set; }

        IItemGraphics ItemGraphics { get; set; }

        dynamic View { get; }

        dynamic ViewControl { get; }

        dynamic ViewSettings { get; }
    }

    public class ViewItemExchangeless : ItemObject, IViewItem
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

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.ItemGraphics.Draw(gameTime, alpha);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.ItemControl.UpdateInput(gameTime);
        }

        public override void UpdateStructureForView(GameTime gameTime)
        {
            this.ItemControl.UpdateStructureForView(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.ItemControl. UpdateStructure(gameTime);
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