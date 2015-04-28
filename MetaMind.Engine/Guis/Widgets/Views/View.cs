namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class View : ViewEntity, IView
    {
        //public View(PointViewSettings1D viewSettings, ItemSettings itemSettings, IViewFactory factory)
        //    : this(viewSettings, itemSettings, factory)
        //{
        //    this.Items = new List<IViewItem>(viewSettings.ColumnNumMax);
        //}

        //public View(PointViewSettings2D viewSettings, ItemSettings itemSettings, IViewFactory factory)
        //    : this(viewSettings, itemSettings, factory)
        //{
        //    this.Items = new List<IViewItem>(viewSettings.RowNumMax * viewSettings.ColumnNumMax);
        //}

        //public View(ContinuousViewSettings viewSettings, ItemSettings itemSettings, IViewFactory factory)
        //    : this(viewSettings, itemSettings, factory)
        //{
        //    this.Items = new List<IViewItem>();
        //}

        protected View(ViewSettings viewSettings, ItemSettings itemSettings, IViewFactory factory)
            : base(viewSettings, itemSettings)
        {
            this.Logic  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Visual = factory.CreateGraphics(this, viewSettings, itemSettings);
        }

        public View(ViewSettings viewSettings, ItemSettings itemSettings, IViewControl logic, IViewVisualControl visual, List<IViewItem> items)
            : base(viewSettings, itemSettings)
        {
            if (logic == null)
            {
                throw new ArgumentNullException("logic");
            }

            if (visual == null)
            {
                throw new ArgumentNullException("visual");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.Items    = items;
            
            this.Logic  = logic;
            this.Visual = visual;
        }

        #region Dependency

        public dynamic Logic { get; set; }

        public IViewVisualControl Visual { get; set; }

        public List<IViewItem> Items { get; set; }

        #endregion

        #region IView

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual.Draw(graphics, time, alpha);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Logic.Update(input, time);
        }

        public override void Update(GameTime time)
        {
            this.Logic .Update(time);
            this.Visual.Update(time);
        }

        #endregion
    }
}