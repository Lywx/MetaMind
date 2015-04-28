namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemExchangeless : ItemEntity, IViewItem
    {
        public ViewItemExchangeless(dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(itemSettings)
        {
            this.View         = view;
            this.ViewSettings = viewSettings;

            this.ItemData   = itemFactory.CreateData(this);
            this.ItemLogic  = itemFactory.CreateLogicControl(this);
            this.ItemVisual = itemFactory.CreateVisualControl(this);
        }

        public ViewItemExchangeless(dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory, dynamic itemData)
            : base(itemSettings)
        {
            this.View         = view;
            this.ViewSettings = viewSettings;

            this.ItemData   = itemData;

            this.ItemLogic  = itemFactory.CreateLogicControl(this);
            this.ItemVisual = itemFactory.CreateVisualControl(this);
        }

        ~ViewItemExchangeless()
        {
            this.Dispose();
        }

        public dynamic ItemLogic { get; set; }

        public dynamic ItemData { get; set; }

        public IItemVisualControl ItemVisual { get; set; }

        public IView View { get; protected set; }

        public dynamic ViewLogic
        {
            get { return this.View.Logic; }
        }

        public dynamic ViewSettings { get; protected set; }

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.ItemVisual.Draw(graphics, time, alpha);
        }

        #endregion

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.ItemLogic .Update(input, time);
            this.ItemVisual.UpdateInput(input, time);
        }

        public override void UpdateView(GameTime gameTime)
        {
            this.ItemLogic.UpdateView(gameTime);
        }

        public override void Update(GameTime time)
        {
            this.ItemLogic .Update(time);
            this.ItemVisual.Update(time);
        }

        public override void Dispose()
        {
            // Don't set item control to null
            // Because the reference is used in the loop that cannot dispose itself
            if (this.ItemLogic != null)
            {
                this.ItemLogic.Dispose();
            }

            base.Dispose();
        }
    }
}