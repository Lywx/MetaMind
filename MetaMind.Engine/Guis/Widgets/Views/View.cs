namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class View : ViewEntity, IView
    {
        public View(ViewSettings viewSettings, IViewLogic viewLogic, IViewVisual viewVisual, IViewLayer viewLayer, ItemSettings itemSettings, List<IViewItem> items)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }

            if (viewLogic == null)
            {
                throw new ArgumentNullException("viewLogic");
            }

            if (viewVisual == null)
            {
                throw new ArgumentNullException("viewVisual");
            }

            if (viewLayer == null)
            {
                throw new ArgumentNullException("viewLayer");
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException("itemSettings");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.ViewSettings = viewSettings;
            this.ViewLogic    = viewLogic;
            this.ViewVisual   = viewVisual;
            this.ViewLayer    = viewLayer;

            this.ViewComponents = new Dictionary<string, object>();

            this.Items        = items;
            this.ItemSettings = itemSettings;
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