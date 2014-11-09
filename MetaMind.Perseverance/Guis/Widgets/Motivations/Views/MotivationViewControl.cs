using System;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewControl : ViewControl1D
    {
        public MotivationViewControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            ItemFactory = new MotivationItemFactory();
        }

        protected MotivationItemFactory ItemFactory { get; set; }

        public void AddItem()
        {
            View.Items.Add( new ViewItemExchangable( View, ViewSettings, ItemSettings, ItemFactory ) );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            base.UpdateInput( gameTime );

            if ( View.IsEnabled( ViewState.View_Active ) &&
                 View.IsEnabled( ViewState.View_Has_Focus ) &&
                !View.IsEnabled( ViewState.Item_Editting ) )
            {
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.CreateItem ) )
                    AddItem();
            }
        }
    }
}