using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Guis.Widgets.FeelingItems;
using MetaMind.Perseverance.Guis.Widgets.FeelingViews;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Feelings
{
    public class FeelingWidget : Widget
    {
        private readonly ViewSettings1D pastViewSettings   = new ViewSettings1D { ColumnNumDisplay = 1, StartPoint = new Point( 160, GraphicsSettings.Height / 2 ) };
        private readonly ViewSettings1D nowViewSettings    = new ViewSettings1D { ColumnNumDisplay = 1, StartPoint = new Point( 421, GraphicsSettings.Height / 2 ) };
        private readonly ViewSettings1D futureViewSettings = new ViewSettings1D { ColumnNumDisplay = 3, StartPoint = new Point( 662, GraphicsSettings.Height / 2 ) };
        
        private IView pastView;
        private IView nowView;
        private IView futureView;

        public FeelingWidget()
        {

            var viewFactory = new FeelingViewFactory();
            pastView        = new View( pastViewSettings,   FeelingItemSettings.Default, viewFactory );
            nowView         = new View( nowViewSettings,    FeelingItemSettings.Default, viewFactory );
            futureView      = new View( futureViewSettings, FeelingItemSettings.Default, viewFactory );

            pastView  .Control.Swap.AddObserver( nowView    );
            pastView  .Control.Swap.AddObserver( futureView );
            nowView   .Control.Swap.AddObserver( pastView   );
            nowView   .Control.Swap.AddObserver( futureView );
            futureView.Control.Swap.AddObserver( pastView   );
            futureView.Control.Swap.AddObserver( nowView    );

            var itemFactory = new FeelingItemFactory();
            pastView  .Items.Add( new ViewItemExchangable( pastView,   pastView  .ViewSettings, pastView  .ItemSettings, itemFactory ) );
            nowView   .Items.Add( new ViewItemExchangable( nowView,    nowView   .ViewSettings, nowView   .ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
            futureView.Items.Add( new ViewItemExchangable( futureView, futureView.ViewSettings, futureView.ItemSettings, itemFactory ) );
        }

        //---------------------------------------------------------------------

        #region Update and Draw

        public override void Draw(GameTime gameTime)
        {
            pastView  .Draw(gameTime);
            nowView   .Draw(gameTime);
            futureView.Draw(gameTime);
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
        }

        #endregion

        //---------------------------------------------------------------------
    }
}
