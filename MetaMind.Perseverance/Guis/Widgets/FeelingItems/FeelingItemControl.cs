using System;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingItems
{
    public class FeelingItemControl : ViewItemControl1D
    {
        private readonly FeelingItemSymbolControl symbolControl;

        public FeelingItemControl( IViewItem item )
            : base( item )
        {
            symbolControl = new FeelingItemSymbolControl( item );
        }

        public FeelingType Type
        {
            get { return symbolControl.Type; }
        }

        public IPickableFrame SymbolFrame
        {
            get { return symbolControl.SymbolFrame; }
        }

        public TimeSpan SelectedTime
        {
            get { return symbolControl.SelectedTime; }
        }

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            symbolControl.Update( gameTime );
        }
    }
}