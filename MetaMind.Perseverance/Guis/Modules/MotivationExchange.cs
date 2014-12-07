namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;

    using Microsoft.Xna.Framework;

    public class MotivationExchange : Module<MotivationExchangeSettings>
    {
        private readonly IView  futureView;
        private readonly IView  nowView;
        private readonly IView  pastView;

        private readonly Banner banner;

        private bool[] readyMoving = new bool[(int)ViewEnum.ViewNum];

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

            // auto-select after startup
            this.nowView.Control.Selection.Select(0);
        }

        private enum ViewEnum
        {
            Past, Now, Future, 

            ViewNum
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

            this.BetweenViewUpdateStructure(gameTime);

            this.banner.Update(gameTime);
        }

        private void BetweenViewUpdateStructure(GameTime gameTime)
        {
            if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
            {
                if (this.IsMoving(ViewEnum.Now))
                {
                    this.nowView   .Control.Selection.Clear();
                    this.nowView   .Control.Region   .Clear();
                    this.pastView  .Control.Selection.Select(0);
                    this.futureView.Control.Selection.Clear();
                    this.futureView.Control.Region   .Clear();
                }
                else if (this.IsMoving(ViewEnum.Future))
                {
                    this.nowView   .Control.Selection.Select(0);
                    this.futureView.Control.Selection.Clear();
                    this.futureView.Control.Region   .Clear();
                }
            }
            else if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
            {
                if (this.IsMoving(ViewEnum.Past))
                {
                    this.pastView.Control.Selection.Clear();
                    this.pastView.Control.Region   .Clear();
                    this.nowView .Control.Selection.Select(0);
                }
                else if (this.IsMoving(ViewEnum.Now))
                {
                    this.pastView  .Control.Selection.Clear();
                    this.pastView  .Control.Region   .Clear();
                    this.nowView   .Control.Selection.Clear();
                    this.nowView   .Control.Region   .Clear();
                    this.futureView.Control.Selection.Select(0);
                }
            }

            this.ResetMoving();

            this.CheckMoving(pastView  , ViewEnum.Past);
            this.CheckMoving(nowView   , ViewEnum.Now);
            this.CheckMoving(futureView, ViewEnum.Future);
        }

        private bool IsMoving(ViewEnum e)
        {
            return this.readyMoving[(int)e];
        }

        private void CompeteMoving(ViewEnum e)
        {
            this.ResetMoving();
            this.EnableMoving(e);
        }

        private void EnableMoving(ViewEnum e)
        {
            this.readyMoving[(int)e] = true;
        }

        private void ResetMoving()
        {
            this.readyMoving = new bool[(int)ViewEnum.ViewNum];
        }

        private void CheckMoving(IView view, ViewEnum e)
        {
            var items = view.Items;
            if (items.Count != 0 && items.First().IsEnabled(ItemState.Item_Selected))
            {
                this.CompeteMoving(e);
            }
        }

        #endregion

    }
}