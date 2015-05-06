namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Items.Visuals;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItem : ItemEntity, IViewItem
    {
        public ViewItem(dynamic view, ItemSettings itemSettings, IViewItemFactory itemFactory)
            : base(itemSettings)
        {
            this.View = view;

            this.ItemData   = itemFactory.CreateData(this);
            this.ItemLogic  = itemFactory.CreateLogic(this);
            this.ItemVisual = itemFactory.CreateVisual(this);
        }

        public ViewItem(dynamic view, ItemSettings itemSettings, IViewItemFactory itemFactory, dynamic itemData)
            : base(itemSettings)
        {
            this.View = view;

            this.ItemData   = itemData;
            this.ItemLogic  = itemFactory.CreateLogic(this);
            this.ItemVisual = itemFactory.CreateVisual(this);
        }

        ~ViewItem()
        {
            this.Dispose();
        }

        #region Direct Dependency

        public IView View { get; protected set; }

        public IViewItemLogic ItemLogic { get; protected set; }

        public dynamic ItemData { get; set; }

        public IViewItemVisual ItemVisual { get; protected set; }

        public IViewItemLayer ItemLayer { get; protected set; }

        #endregion

        #region Events

        public event EventHandler<EventArgs> Selected;

        public event EventHandler<EventArgs> Unselected;

        public event EventHandler<EventArgs> Swapped;

        public event EventHandler<EventArgs> Transited;

        internal void OnSwapped()
        {
            if (this.Swapped != null)
            {
                this.Swapped(this, EventArgs.Empty);
            }
        }

        internal void OnTransited()
        {
            if (this.Transited != null)
            {
                this.Transited(this, EventArgs.Empty);
            }
        }

        internal void OnUnselected()
        {
            if (this.Unselected != null)
            {
                this.Unselected(this, EventArgs.Empty);
            }
        }

        internal void OnSelected()
        {
            if (this.Selected != null)
            {
                this.Selected(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.ItemVisual.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.ItemLogic.UpdateInput(input, time);
            this.ItemVisual.UpdateInput(input, time);
        }

        public override void UpdateView(GameTime gameTime)
        {
            this.ItemLogic.UpdateView(gameTime);
        }

        public override void Update(GameTime time)
        {
            this.ItemLogic.Update(time);
            this.ItemVisual.Update(time);
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            if (this.ItemLogic != null)
            {
                this.ItemLogic.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}