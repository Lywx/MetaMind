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

            this.Logic  = viewFactory.CreateLogicControl(this, viewSettings, itemSettings);
            this.Visual = viewFactory.CreateVisualControl(this, viewSettings, itemSettings);
        }
        
        #region Dependency

        public List<IViewItem> Items { get; set; }

        public dynamic Logic { get; set; }

        public IViewVisualControl Visual { get; set; }

        #endregion

        #region IView

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.Visual != null)
            {
                this.Visual.Draw(graphics, time, alpha);
            }
        }

        public override void Update(GameTime time)
        {
            if (this.Logic != null)
            {
                this.Logic.Update(time);
            }

            if (this.Visual != null)
            {
                this.Visual.Update(time);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.Logic != null)
            {
                this.Logic.Update(input, time);
            }
        }

        #endregion
    }
}