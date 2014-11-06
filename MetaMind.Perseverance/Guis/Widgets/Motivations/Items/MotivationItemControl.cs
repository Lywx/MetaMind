using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemControl : ViewItemControl1D
    {
        private MotivationItemSymbolControl ItemSymbolControl { get; set; }

        public MotivationItemControl( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new MotivationItemSymbolFrameControl( item );
            ItemSymbolControl = new MotivationItemSymbolControl( item );
            ItemDataControl = new ViewItemDataControl( item );
        }

        public IPickableFrame SymbolFrame
        {
            get { return ItemFrameControl.SymbolFrame; }
        }

        protected override void UpdateInput( GameTime gameTime )
        {
            base             .UpdateInput( gameTime );
            ItemSymbolControl.Update( gameTime );
        }
    }
}