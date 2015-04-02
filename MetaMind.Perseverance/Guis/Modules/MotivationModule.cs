namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MotivationModule : Module<MotivationModuleSettings>
    {
        private readonly IView  futureView;
        private readonly IView  nowView;
        private readonly IView  pastView;

        private readonly Banner banner;

        private MotivationModuleGameStartedListener gameStartedListener;

        private bool[] readyMoving = new bool[(int)ViewEnum.ViewNum];

        public MotivationModule(MotivationModuleSettings settings)
            : base(settings)
        {
            this.banner = new Banner(this.Settings.PastViewSettings, new BannerSetting());

            this.pastView   = new PointView(this.Settings.PastViewSettings  , Settings.ItemSettings, Settings.ViewFactory);
            this.nowView    = new PointView(this.Settings.NowViewSettings   , Settings.ItemSettings, Settings.ViewFactory);
            this.futureView = new PointView(this.Settings.FutureViewSettings, Settings.ItemSettings, Settings.ViewFactory);

            this.pastView  .Control.Swap.AddObserver(this.nowView   );
            this.pastView  .Control.Swap.AddObserver(this.futureView);
            this.nowView   .Control.Swap.AddObserver(this.pastView  );
            this.nowView   .Control.Swap.AddObserver(this.futureView);
            this.futureView.Control.Swap.AddObserver(this.pastView  );
            this.futureView.Control.Swap.AddObserver(this.nowView   );
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
            foreach (var entry in MotivationModuleSettings.GetPastMotivations())
            {
                this.pastView.Control.AddItem(entry);
            }

            foreach (var entry in MotivationModuleSettings.GetNowMotivations())
            {
                this.nowView.Control.AddItem(entry);
            }

            foreach (var entry in MotivationModuleSettings.GetFutureMotivations())
            {
                this.futureView.Control.AddItem(entry);
            }

            this.LoadEvents();
        }

        private void LoadEvents()
        {
            if (this.gameStartedListener == null)
            {
                this.gameStartedListener = new MotivationModuleGameStartedListener(this.nowView);
            }

            EventManager.AddListener(this.gameStartedListener);
        }

        public override void Unload()
        {
            this.UnloadEvents();
        }

        private void UnloadEvents()
        {
            if (this.gameStartedListener == null)
            {
                EventManager.RemoveListener(this.gameStartedListener);
            }

            this.gameStartedListener = null;
        }

        #endregion

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.pastView  .Draw(gameTime, alpha);
            this.nowView   .Draw(gameTime, alpha);
            this.futureView.Draw(gameTime, alpha);

            this.banner    .Draw(gameTime, alpha);
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
                if (this.IsMoving(ViewEnum.Now) && this.nowView.Control.AcceptInput)
                {
                    this.nowView   .Control.Selection.Clear();
                    this.nowView   .Control.Region   .Clear();
                    this.pastView  .Control.Selection.Select(0);
                    this.futureView.Control.Selection.Clear();
                    this.futureView.Control.Region   .Clear();
                }
                else if (this.IsMoving(ViewEnum.Future) && this.futureView.Control.AcceptInput)
                {
                    this.nowView   .Control.Selection.Select(0);
                    this.futureView.Control.Selection.Clear();
                    this.futureView.Control.Region   .Clear();
                }
            }
            else if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
            {
                if (this.IsMoving(ViewEnum.Past) && this.pastView.Control.AcceptInput)
                {
                    this.pastView.Control.Selection.Clear();
                    this.pastView.Control.Region   .Clear();
                    this.nowView .Control.Selection.Select(0);
                }
                else if (this.IsMoving(ViewEnum.Now) && this.nowView.Control.AcceptInput)
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
            if (items.Count != 0 && 
                items.First().IsEnabled(ItemState.Item_Selected))
            {
                this.CompeteMoving(e);
            }
        }

        #endregion
    }
}