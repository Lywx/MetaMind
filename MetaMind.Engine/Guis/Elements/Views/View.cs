namespace MetaMind.Engine.Guis.Elements.Views
{
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    public interface IView : IViewObject
    {
        dynamic Control { get; set; }

        IViewGraphics Graphics { get; set; }

        List<IViewItem> Items { get; set; }
    }

    public class View : ViewObject, IView
    {
        public View(ICloneable viewSettings, ICloneable itemSettings, IViewFactory factory)
            : base(viewSettings, itemSettings)
        {
            this.Items = new List<IViewItem>();

            this.Control = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);
        }

        public dynamic Control { get; set; }

        public IViewGraphics Graphics { get; set; }

        public List<IViewItem> Items { get; set; }

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
            this.Control.UpdateStructure(gameTime);
            this.Graphics.Update(gameTime);
        }
    }
}