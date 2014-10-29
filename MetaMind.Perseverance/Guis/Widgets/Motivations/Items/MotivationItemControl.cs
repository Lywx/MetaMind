using System;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemControl : ViewItemControl1D
    {
        private readonly MotivationItemSymbolControl symbolControl;

        public MotivationItemControl( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new MotivationItemSymbolFrameControl( item );

            symbolControl    = new MotivationItemSymbolControl( item );
        }

        public MotivationType Type
        {
            get { return symbolControl.Type; }
        }

        public IPickableFrame SymbolFrame
        {
            get { return ( ( MotivationItemSymbolFrameControl ) ItemFrameControl ).SymbolFrame; }
        }

        public override void Update( GameTime gameTime )
        {
            base         .Update( gameTime );
            symbolControl.Update( gameTime );
        }
    }
}