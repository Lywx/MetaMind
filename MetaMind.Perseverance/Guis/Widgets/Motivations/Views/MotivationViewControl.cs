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
            if ( View.IsEnabled( ViewState.View_Active ) &&
                 View.IsEnabled( ViewState.View_Has_Focus ) &&
                !View.IsEnabled( ViewState.Item_Editting ) )
            {
                //------------------------------------------------------------------
                // mouse
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                {
                    Scroll.MoveLeft();
                }
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                {
                    Scroll.MoveRight();
                }
                //------------------------------------------------------------------
                // keyboard
                {
                    //--------------------------------------------------------------
                    // screen movement
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                    {
                        MoveLeft();
                    }
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                    {
                        MoveRight();
                    }
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.SLeft ) )
                    {
                        for ( var i = 0 ; i < ViewSettings.ColumnNumDisplay; i++ )
                        {
                            MoveLeft();
                        }
                    }
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.SRight ) )
                    {
                        for ( var i = 0 ; i < ViewSettings.ColumnNumDisplay; i++ )
                        {
                            MoveRight();
                        }
                    }
                    //--------------------------------------------------------------
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                    {
                        Selection.Clear();
                    }
                    //--------------------------------------------------------------
                    // list management
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.MotivationCreateItem ) )
                    {
                        AddItem();
                    }
                }  
            }
            //-----------------------------------------------------------------
            // item input
            foreach ( var item in View.Items.ToArray() )
            {
                item.UpdateInput( gameTime );
            }
        }

        private void MoveRight()
        {
            if ( ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left )
            {
                // invert for left scrolling view
                Selection.MoveLeft();
            }
            else
            {
                Selection.MoveRight();
            }
        }

        private void MoveLeft()
        {
            if ( ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left )
            {
                // invert for left scrolling view
                Selection.MoveRight();
            }
            else
            {
                Selection.MoveLeft();
            }
        }
    }
}