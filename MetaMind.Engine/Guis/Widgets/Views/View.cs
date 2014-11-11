using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IView : IViewObject
    {
        dynamic         Control  { get; set; }
        IViewGraphics   Graphics { get; set; }
        List<IViewItem> Items    { get; set; }
    }

    public class View : ViewObject, IView
    {
        public View( ICloneable viewSettings, ICloneable itemSettings, IViewFactory factory )
            : base( viewSettings, itemSettings )
        {
            Items    = new List<IViewItem>();

            Control  = factory.CreateControl( this, viewSettings, itemSettings );
            Graphics = factory.CreateGraphics( this, viewSettings, itemSettings );
        }

        public dynamic         Control  { get; set; }
        public IViewGraphics   Graphics { get; set; }
        public List<IViewItem> Items    { get; set; }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            Graphics.Draw( gameTime, alpha );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            Control.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            Control .UpdateStrucutre( gameTime );
            Graphics.Update( gameTime );
        }
    }
}