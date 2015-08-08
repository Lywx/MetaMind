namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Concepts.Operations;
    using Concepts.Synchronizations;
    using Concepts.Tests;
    using Engine.Components.Fonts;
    using Engine.Components.Inputs;
    using Engine.Guis.Layers;
    using Engine.Guis.Widgets.Buttons;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Screens;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Layers;
    using Microsoft.Xna.Framework;
    using Scripting;

    public class MainScreen : GameScreen
    {
        private Button buttonPrevious;

        private Button buttonNext;

        private Label screenLabel;

        private SynchronizationSession synchronizationSession;

        private TestSession testSession;

        private OperationSession operationSession;

        private ScriptSearcher scriptSearcher;

        private ScriptRunner scriptRunner;

        public MainScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.IsPopup = true;
        }

        public CircularGameLayer CircularLayers { get; private set; }

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            // Script runner
            this.scriptSearcher = new ScriptSearcher();
            this.scriptRunner   = new ScriptRunner(this.scriptSearcher, Unity.FsiSession);
            this.scriptRunner.Search();

            // Synchronization session
            this.synchronizationSession = new SynchronizationSession();

            // Test session
            this.testSession = new TestSession(Unity.FsiSession);

            // Operation session
            this.operationSession = new OperationSession(Unity.FsiSession);

            // Buttons
            var graphics = interop.Engine.Graphics;

            const int buttonWidth = 30;
            const int buttonHeight = 300;
            var buttonY = (graphics.Settings.Height - buttonHeight) / 2;
            var buttonSettings = new ButtonSettings
            {
                BoundaryRegularColor      = Palette.Transparent0,
                BoundaryMouseOverColor    = Palette.DimBlue,
                BoundaryMousePressedColor = Palette.Transparent6,
                MouseOverColor            = Palette.Transparent1,
                MousePressedColor         = Palette.Transparent6
            };
            this.buttonPrevious = new Button(
                new Rectangle(0, buttonY, buttonWidth, buttonHeight),
                buttonSettings)
            {
                MouseLeftPressedAction = () => this.CircularLayers.PreviousLayer(),
                Label =
                {
                    TextFont   = () => Font.UiRegular,
                    Text       = () => "<",
                    TextColor  = () => Color.White,
                    TextSize   = () => 1f,
                },
            };

            this.buttonNext = new Button(
                new Rectangle(graphics.Settings.Width - buttonWidth, buttonY, buttonWidth, buttonHeight), buttonSettings)
            {
                MouseLeftPressedAction = () => this.CircularLayers.NextLayer(),
                Label =
                {
                    TextFont   = () => Font.UiRegular,
                    Text       = () => ">",
                    TextColor  = () => Color.White,
                    TextSize   = () => 1f,
                }
            };

            this.screenLabel = new Label
            {
                TextFont     = () => Font.UiRegular,
                Text         = () => this.CircularLayers.GameLayerDisplayed is TestLayer ? "Tests" : "Operations",
                TextPosition = () => new Vector2(40, 25),
                TextColor    = () => Palette.Transparent5,
                TextSize     = () => 1f,
            };

            this.Layers.Add(new SynchronizationLayer(this));

            this.CircularLayers = new CircularGameLayer(this);
            this.CircularLayers.Add(new TestLayer(this.testSession, this));
            this.CircularLayers.Add(new OperationLayer(this.operationSession, this));
            this.Layers.Add(this.CircularLayers);

            // First run of fsi session
            this.RunScripts();

            base.LoadContent(interop);
        }

        #endregion

        public override void Update(GameTime time)
        {
            this.buttonPrevious.Update(time);
            this.buttonNext    .Update(time);
            this.screenLabel   .Update(time);

            base.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.SessionRerun))
            {
                this.RunScripts();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.SynchronizationPause))
            {
                this.synchronizationSession.ToggleSynchronization();
                this.operationSession      .ToggleNotification();
                this.testSession.           ToggleNotification();
            }

            this.buttonPrevious.UpdateInput(input, time);
            this.buttonNext    .UpdateInput(input, time);

            base.UpdateInput(input, time);
        }

        private void RunScripts()
        {
            Unity.SessionData.Operation.Reset();
            Unity.SessionData.Test     .Reset();

            this.scriptRunner.Rerun();
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            graphics.SpriteBatch.Begin();

            // Buttons have the same alpha value as circular layer
            this.buttonPrevious.Draw(graphics, time, Math.Min(this.TransitionAlpha, this.CircularLayers.TransitionAlpha));
            this.buttonNext    .Draw(graphics, time, Math.Min(this.TransitionAlpha, this.CircularLayers.TransitionAlpha));
            this.screenLabel   .Draw(graphics, time, Math.Min(this.TransitionAlpha, this.CircularLayers.TransitionAlpha));

            graphics.SpriteBatch.End();

            base.Draw(graphics, time);
        }
    }
}