namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;

    using Microsoft.Xna.Framework;

    public class MotivationExchange : Module<MotivationExchangeSettings>
    {
        private readonly IView  futureView;
        private readonly IView  nowView;
        private readonly IView  pastView;
        
        private readonly Banner banner;

        public MotivationExchange(MotivationExchangeSettings settings)
            : base(settings)
        {
            this.banner = new Banner(this.Settings.PastViewSettings, new BannerSetting());

            this.pastView   = new View(this.Settings.PastViewSettings  , Settings.ItemSettings, Settings.ViewFactory);
            this.nowView    = new View(this.Settings.NowViewSettings   , Settings.ItemSettings, Settings.ViewFactory);
            this.futureView = new View(this.Settings.FutureViewSettings, Settings.ItemSettings, Settings.ViewFactory);

            this.pastView  .Control.Swap.AddObserver(this.nowView   );
            this.pastView  .Control.Swap.AddObserver(this.futureView);
            this.nowView   .Control.Swap.AddObserver(this.pastView  );
            this.nowView   .Control.Swap.AddObserver(this.futureView);
            this.futureView.Control.Swap.AddObserver(this.pastView  );
            this.futureView.Control.Swap.AddObserver(this.nowView   );
        }

        #region Load and Unload

        public override void Load()
        {
            // performance penalty is not severe for one-off loading
            foreach (var entry in MotivationExchangeSettings.GetPastMotivations())
            {
                this.pastView.Control.AddItem(entry);
            }

            foreach (var entry in MotivationExchangeSettings.GetNowMotivations())
            {
                this.nowView.Control.AddItem(entry);
            }

            foreach (var entry in MotivationExchangeSettings.GetFutureMotivations())
            {
                this.futureView.Control.AddItem(entry);
            }
        }

        public override void Unload()
        {
        }

        #endregion

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.pastView  .Draw(gameTime, alpha);
            this.nowView   .Draw(gameTime, alpha);
            this.futureView.Draw(gameTime, alpha);

            this.banner.Draw(gameTime, alpha);
        }


        public override void HandleInput()
        {
            base.HandleInput();
            
            this.pastView  .HandleInput();
            this.nowView   .HandleInput();
            this.futureView.HandleInput();
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.pastView  .UpdateInput(gameTime);
            this.nowView   .UpdateInput(gameTime);
            this.futureView.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.pastView  .UpdateStructure(gameTime);
            this.nowView   .UpdateStructure(gameTime);
            this.futureView.UpdateStructure(gameTime);

            this.banner.Update(gameTime);
        }

        #endregion
    }
}