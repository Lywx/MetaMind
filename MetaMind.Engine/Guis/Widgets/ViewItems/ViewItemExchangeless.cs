using System;
using MetaMind.Engine.Guis.Widgets.Items;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItem : IItemObject
    {
        dynamic       ItemData     { get; }
        dynamic       ItemControl  { get; set; }
        IItemGraphics ItemGraphics { get; set; }
        dynamic       View         { get; }
        dynamic       ViewControl  { get; }
        dynamic       ViewSettings { get; }
    }

    public class ViewItemExchangeless : ItemObject, IViewItem
    {
        public ViewItemExchangeless( dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory )
            : base( itemSettings )
        {
            View         = view;
            ViewSettings = viewSettings;

            ItemData     = itemFactory.CreateData( this );
            ItemControl  = itemFactory.CreateControl( this );
            ItemGraphics = itemFactory.CreateGraphics( this );
        }

        public ViewItemExchangeless( dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory, dynamic itemData )
            : base( itemSettings )
        {
            View         = view;
            ViewSettings = viewSettings;

            ItemData     = itemData;
            ItemControl  = itemFactory.CreateControl( this );
            ItemGraphics = itemFactory.CreateGraphics( this );
        }

        public dynamic       ItemData { get; set; }
        public dynamic       ItemControl { get; set; }
        public IItemGraphics ItemGraphics { get; set; }

        public dynamic View { get; protected set; }

        public dynamic ViewControl { get { return View.Control; } }

        public dynamic ViewSettings { get; protected set; }

        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime )
        {
            ItemGraphics.Draw( gameTime );
        }

        public override void Update( Microsoft.Xna.Framework.GameTime gameTime )
        {
            ItemControl .Update( gameTime );
            ItemGraphics.Update( gameTime );
        }
    }
}