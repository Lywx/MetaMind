using System;
using MetaMind.Engine.Guis.Widgets.Items;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItem : IItemObject
    {
        IViewItemData ItemData { get; }
        dynamic ItemControl { get; set; }
        IItemGraphics ItemGraphics { get; set; }
        dynamic View { get; }
        dynamic ViewControl { get; }
        dynamic ViewSettings { get; }
    }

    public class ViewItemExchangeless : ItemObject, IViewItem
    {
        private dynamic view;
        private dynamic viewSettings;

        public ViewItemExchangeless( dynamic view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory )
            : base( itemSettings )
        {
            this.view         = view;
            this.viewSettings = viewSettings;

            ItemData     = itemFactory.CreateData( this );
            ItemControl  = itemFactory.CreateControl( this );
            ItemGraphics = itemFactory.CreateGraphics( this );
        }

        public IViewItemData ItemData { get; set; }
        public dynamic       ItemControl { get; set; }
        public IItemGraphics ItemGraphics { get; set; }

        public dynamic View
        {
            get { return view; }
            protected set { view = value; }
        }

        public dynamic ViewControl { get { return view.Control; } }

        public dynamic ViewSettings
        {
            get { return viewSettings; }
            protected set { viewSettings = value; }
        }

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