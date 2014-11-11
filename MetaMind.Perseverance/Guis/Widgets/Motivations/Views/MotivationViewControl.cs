using System;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewControl : ViewControl1D
    {
        public MotivationViewControl( IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            ItemFactory = new MotivationItemFactory();
        }

        public MotivationItemFactory ItemFactory { get; protected set; }

        #region Operations

        public void AddItem( MotivationEntry entry )
        {
            View.Items.Add( new ViewItemExchangable( View, ViewSettings, ItemSettings, ItemFactory, entry ) );
        }

        public void AddItem()
        {
            View.Items.Add( new ViewItemExchangable( View, ViewSettings, ItemSettings, ItemFactory ) );
        }

        #endregion

        #region Update

        public override void UpdateInput( GameTime gameTime )
        {
            if ( AcceptInput )
            {
                // mouse
                //------------------------------------------------------------------
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                {
                    Scroll.MoveLeft();
                }
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                {
                    Scroll.MoveRight();
                }

                // keyboard
                //------------------------------------------------------------------
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
                    for ( var i = 0 ; i < ViewSettings.ColumnNumDisplay ; i++ )
                    {
                        MoveLeft();
                    }
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.SRight ) )
                {
                    for ( var i = 0 ; i < ViewSettings.ColumnNumDisplay ; i++ )
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

            // item input
            //-----------------------------------------------------------------
            foreach ( var item in View.Items.ToArray() )
            {
                item.UpdateInput( gameTime );
            }
        }

        #endregion
    }
}