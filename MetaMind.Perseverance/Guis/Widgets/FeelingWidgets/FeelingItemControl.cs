using System;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingWidgets
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

        protected override void UpdateStructure(GameTime gameTime)
        {
            base.UpdateStructure( gameTime );
            symbolControl.Update( gameTime );
        }

        protected override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

        }
    }
}