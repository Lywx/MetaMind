namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Sessions;

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
        public override void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
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

            new Listener(new List<int> { (int)SessionEventType.GameStarted },
                e => {
                        // auto-select after startup
                        this.intelligence.Control.Selection.Select(0);

                        return true;
                    });

            gameInterop.Events.AddListener(this.gameStartedListener);
        }

        public override void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.UnloadInterop(gameInterop);
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.intelligence.Draw(gameGraphics, gameTime, alpha);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.intelligence.UpdateInput(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.intelligence.Update(gameTime);
        }

        #endregion Update and Draw
    }
}