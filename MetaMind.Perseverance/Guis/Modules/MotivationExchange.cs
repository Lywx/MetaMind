using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class MotivationExchange : Module<MotivationExchangeSettings>
    {
        private readonly IView  futureView;
        private readonly IView  nowView;
        private readonly IView  pastView;
        
        private readonly Banner viewBanner;

        public MotivationExchange( MotivationExchangeSettings settings )
            : base( settings )
        {
            viewBanner = new Banner( Settings.PastViewSettings );

            pastView   = new View( Settings.PastViewSettings  , Settings.ItemSettings, Settings.ViewFactory );
            nowView    = new View( Settings.NowViewSettings   , Settings.ItemSettings, Settings.ViewFactory );
            futureView = new View( Settings.FutureViewSettings, Settings.ItemSettings, Settings.ViewFactory );
            
            pastView  .Control.Swap.AddObserver( nowView    );
            pastView  .Control.Swap.AddObserver( futureView );
            nowView   .Control.Swap.AddObserver( pastView   );
            nowView   .Control.Swap.AddObserver( futureView );
            futureView.Control.Swap.AddObserver( pastView   );
            futureView.Control.Swap.AddObserver( nowView    );

            pastView  .Items.Add( new ViewItemExchangable( pastView,   pastView  .ViewSettings, pastView  .ItemSettings, Settings.ItemFactory ) );
            nowView   .Items.Add( new ViewItemExchangable( nowView,    nowView   .ViewSettings, nowView   .ItemSettings, Settings.ItemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, Settings.ItemFactory ) );
        }

        public override void Load()
        {
            foreach ( var entry in Settings.GetPastMotivations() )
            {
                pastView.Control.AddItem( entry );
            }
            foreach ( var entry in Settings.GetNowMotivations() )
            {
                nowView.Control.AddItem( entry );
            }
            foreach ( var entry in Settings.GetFutureMotivations() )
            {
                futureView.Control.AddItem( entry );
            }
        }

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
            base      .HandleInput();
            
            pastView  .HandleInput();
            nowView   .HandleInput();
            futureView.HandleInput();
        }

        public override void UpdateInput( GameTime gameTime )
        {
            pastView  .UpdateInput( gameTime );
            nowView   .UpdateInput( gameTime );
            futureView.UpdateInput( gameTime );
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            pastView  .UpdateStructure( gameTime );
            nowView   .UpdateStructure( gameTime );
            futureView.UpdateStructure( gameTime );

            viewBanner.Update( gameTime );
        }

        #endregion
    }
}