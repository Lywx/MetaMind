namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class View : ViewEntity, IView
    {
        public View(ICloneable viewSettings, IViewFactory viewFactory, ICloneable itemSettings, List<IViewItem> items)
            : base(viewSettings, itemSettings)
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

            this.Items = items;

            // Composition root of view
            this.ViewLogic  = viewFactory.CreateLogicControl(this, viewSettings, itemSettings);
            this.ViewVisual = viewFactory.CreateVisualControl(this, viewSettings, itemSettings);
        }
        
        #region Dependency

        public List<IViewItem> Items { get; set; }

        public dynamic ViewLogic { get; set; }

        public IViewVisualControl ViewVisual { get; set; }

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
                ((IUpdateable)this.ViewLogic).Update(time);
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
                ((IInputable)this.ViewLogic).UpdateInput(input, time);
            }
        }

        #endregion
    }
}