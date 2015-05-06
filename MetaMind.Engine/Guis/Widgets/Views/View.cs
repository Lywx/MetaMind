namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class View : ViewEntity, IView
    {
        public View(ViewSettings viewSettings, IViewFactory viewFactory, ItemSettings itemSettings, List<IViewItem> items)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }

            if (viewFactory == null)
            {
                throw new ArgumentNullException("viewFactory");
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException("itemSettings");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.Items        = items;
            this.ItemSettings = itemSettings;

            // Composition root of view
            this.ViewLogic  = viewFactory.CreateLogic(this, viewSettings, itemSettings);
            this.ViewVisual = viewFactory.CreateVisual(this, viewSettings, itemSettings);
            this.ViewLayer  = viewFactory.CreateExtension(this);

            this.ViewComponents = new Dictionary<string, object>();
        }
        
        #region Dependency

        public IViewLayer ViewLayer { get; private set; }

        public Dictionary<string, object> ViewComponents { get; private set; }

        public ViewSettings ViewSettings { get; set; }

        public IViewLogic ViewLogic { get; set; }

        public IViewVisual ViewVisual { get; set; }

        public List<IViewItem> Items { get; set; }

        public ItemSettings ItemSettings { get; set; }

        #endregion

        #region IView

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.ViewVisual != null)
            {
                this.ViewVisual.Draw(graphics, time, alpha);
            }
        }

        public override void Update(GameTime time)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.Update(time);
            }

            if (this.ViewVisual != null)
            {
                this.ViewVisual.Update(time);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.UpdateInput(input, time);
            }
        }

        #endregion
    }
}