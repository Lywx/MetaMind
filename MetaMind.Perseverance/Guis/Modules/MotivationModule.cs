namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class MotivationModule : Module<MotivationModuleSettings>
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

        // FIXME: Wrong interface
        public override void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
            base.Load();
            // performance penalty is not severe for one-off loading
            foreach (var entry in MotivationModuleSettings.GetNowMotivations())
            {
                this.intelligence.Control.AddItem(entry);
            }

            this.LoadEvents(gameInterop);
        }

        private void LoadEvents(IGameInterop gameInterop)
        {
            if (this.gameStartedListener == null)
            {
                this.gameStartedListener = new MotivationModuleGameStartedListener(this.intelligence);
            }

            gameInterop.Event.AddListener(this.gameStartedListener);
        }

        public override void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
            this.UnloadEvents(gameInterop);
        }

        private void UnloadEvents(IGameInterop gameInterop)
        {
            if (this.gameStartedListener == null)
            {
                gameInterop.Event.RemoveListener(this.gameStartedListener);
            }

            this.gameStartedListener = null;
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.intelligence.Draw(gameGraphics, gameTime, alpha);
        }

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            this.intelligence.Update(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.intelligence.Update(gameTime);
        }

        #endregion Update and Draw
    }
}