using System;
using System.Collections.Generic;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IView : IViewObject
    {
        dynamic Control { get; set; }
        IViewGraphics Graphics { get; set; }
        List<IViewItem> Items { get; set; }
    }

    public class View : ViewObject, IView
    {
        private dynamic control;
        private IViewGraphics graphics;

        private List<IViewItem> items;

        public View( ICloneable viewSettings, ICloneable itemSettings, IViewFactory factory )
            : base( viewSettings, itemSettings )
        {
            items = new List<IViewItem>();

            control  = factory.CreateControl( this, viewSettings, itemSettings );
            graphics = factory.CreateGraphics( this, viewSettings, itemSettings );
        }

        public dynamic Control
        {
            get { return control; }
            set { control = value; }
        }
        public IViewGraphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        public List<IViewItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            graphics.Draw( gameTime );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            control.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            control .UpdateStrucutre( gameTime );
            graphics.Update( gameTime );
        }
    }
}