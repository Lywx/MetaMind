using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations
{
    public class MotivationExchange : Widget
    {
        private readonly ViewSettings1D pastViewSettings;
        private readonly ViewSettings1D nowViewSettings;
        private readonly ViewSettings1D futureViewSettings;

        private readonly IView pastView;
        private readonly IView nowView;
        private readonly IView futureView;
        
        private readonly MotivationViewFactory  viewFactory;
        private readonly MotivationItemSettings itemSettings;
        
        private readonly Banner viewBanner;

        public MotivationExchange()
        {
            pastViewSettings = new ViewSettings1D
            {
                ColumnNumDisplay = 10,
                StartPoint = new Point( 160, GraphicsSettings.Height / 2 ),
                Direction = ViewSettings1D.ScrollDirection.Left
            };
            nowViewSettings = new ViewSettings1D
            {
                ColumnNumDisplay = 1,
                StartPoint = new Point( 421, GraphicsSettings.Height / 2 )
            };
            futureViewSettings = new ViewSettings1D
            {
                ColumnNumDisplay = 10,
                StartPoint = new Point( 662, GraphicsSettings.Height / 2 )
            };
            
            viewFactory  = new MotivationViewFactory();
            itemSettings = new MotivationItemSettings();

            pastView   = new View( pastViewSettings,   itemSettings, viewFactory );
            nowView    = new View( nowViewSettings,    itemSettings, viewFactory );
            futureView = new View( futureViewSettings, itemSettings, viewFactory );

            pastView  .Control.Swap.AddObserver( nowView    );
            pastView  .Control.Swap.AddObserver( futureView );
            nowView   .Control.Swap.AddObserver( pastView   );
            nowView   .Control.Swap.AddObserver( futureView );
            futureView.Control.Swap.AddObserver( pastView   );
            futureView.Control.Swap.AddObserver( nowView    );

            var itemFactory = new MotivationItemFactory();
            pastView  .Items.Add( new ViewItemExchangable( pastView,   pastView  .ViewSettings, pastView  .ItemSettings, itemFactory ) );
            pastView  .Items.Add( new ViewItemExchangable( pastView,   pastView  .ViewSettings, pastView  .ItemSettings, itemFactory ) );
            pastView  .Items.Add( new ViewItemExchangable( pastView,   pastView  .ViewSettings, pastView  .ItemSettings, itemFactory ) );
            nowView   .Items.Add( new ViewItemExchangable( nowView,    nowView   .ViewSettings, nowView   .ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );

            //-----------------------------------------------------------------
            viewBanner = new Banner( pastViewSettings );
        }

        //---------------------------------------------------------------------

        #region Update and Draw

        public override void Draw( GameTime gameTime, byte alpha )
        {
            pastView  .Draw( gameTime, alpha );
            nowView   .Draw( gameTime, alpha );
            futureView.Draw( gameTime, alpha );
            
            viewBanner.Draw( gameTime, alpha );
        }


        public override void HandleInput()
        {
            base.HandleInput();

            pastView  .HandleInput();
            nowView   .HandleInput();
            futureView.HandleInput();
        }

        public override void UpdateInput(GameTime gameTime)
        {
            pastView  .UpdateInput(gameTime);
            nowView   .UpdateInput(gameTime);
            futureView.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            pastView  .UpdateStructure(gameTime);
            nowView   .UpdateStructure(gameTime);
            futureView.UpdateStructure(gameTime);

            viewBanner.Update( gameTime );
        }

        #endregion

        //---------------------------------------------------------------------
    }
}