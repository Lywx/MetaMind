namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
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
        public override void Load(IGameInputService input, IGameInteropService interop)
        {
            // performance penalty is not severe for one-off loading
            foreach (var entry in MotivationModuleSettings.GetNowMotivations())
            {
                this.intelligence.Control.AddItem(entry);
            }

            this.LoadEvents(interop);
        }

        private void LoadEvents(Engine.Services.IGameInteropService interop)
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

            interop.Event.AddListener(this.gameStartedListener);
        }

        public override void Unload(IGameInputService input, IGameInteropService interop)
        {
            this.UnloadInterop(interop);
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.intelligence.Draw(graphics, time, alpha);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.intelligence.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.intelligence.Update(time);
        }

        #endregion Update and Draw
    }
}