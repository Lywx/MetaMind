namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class MotivationModule : Module<MotivationModuleSettings>, IModule
    {
        private readonly IView intelligence;
        private readonly IView intime;

        private MotivationModuleGameStartedListener gameStartedListener;

        public MotivationModule(MotivationModuleSettings settings)
            : base(settings)
        {
            this.intelligence = new PointView(
                this.Settings.IntelligenceViewSettings,
                Settings.ItemSettings,
                Settings.ViewFactory);

            var svs = new ContinuousViewSettings();
            var isettings = new ItemSettings();
            //var ifactory = new ContinuousViewFactory();
            //timeline = new ContinuousView(svs, isettings, ifactory);
        }

        #region Load and Unload

        void IModule.Load()
        {
            // performance penalty is not severe for one-off loading
            foreach (var entry in MotivationModuleSettings.GetNowMotivations())
            {
                this.intelligence.Control.AddItem(entry);
            }

            this.LoadEvents();
        }

        private void LoadEvents()
        {
            if (this.gameStartedListener == null)
            {
                this.gameStartedListener = new MotivationModuleGameStartedListener(this.intelligence);
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

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.intelligence.Draw(gameTime, alpha);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            this.intelligence.HandleInput();
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.intelligence.UpdateInput(gameInput, gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.intelligence.UpdateStructure(gameTime);
        }

        #endregion Update and Draw
    }
}