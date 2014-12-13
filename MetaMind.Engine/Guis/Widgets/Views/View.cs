namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    using Microsoft.Xna.Framework;

    public interface IView : IViewObject
    {
        dynamic Control { get; set; }

        IViewGraphics Graphics { get; set; }

        List<IViewItem> Items { get; set; }
    }

    public class View : ViewObject, IView
    {
        public View(ViewSettings1D viewSettings, ItemSettings itemSettings, IViewFactory factory, dynamic parent = null)
            : base(viewSettings, itemSettings)
        {
            this.Items = new List<IViewItem>(viewSettings.ColumnNumMax);

            this.Control  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);

            this.Parent = parent;
        }

        public View(ViewSettings2D viewSettings, ItemSettings itemSettings, IViewFactory factory, dynamic parent = null)
            : base(viewSettings, itemSettings)
        {
            this.Items = new List<IViewItem>(viewSettings.RowNumMax * viewSettings.ColumnNumMax);

            this.Control  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);

            this.Parent = parent;
        }

        public dynamic Control { get; set; }

        public IViewGraphics Graphics { get; set; }

        public List<IViewItem> Items { get; set; }

        public dynamic Parent { get; private set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.Graphics.Draw(gameTime, alpha);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.Control.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.Control .UpdateStructure(gameTime);
            this.Graphics.UpdateStructure(gameTime);
        }
    }
}