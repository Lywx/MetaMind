namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Concepts.Operations;
    using Concepts.Synchronizations;
    using Concepts.Tests;
    using Engine.Components.Content.Fonts;
    using Engine.Gui.Controls.Buttons;
    using Engine.Screen;
    using Engine.Service;
    using Engine.Service.Scripting.FSharp;
    using Engine.Settings.Color;
    using Layers;
    using Microsoft.Xna.Framework;

    public class MainScreen : MMScreen
    {
        private RectangleButton buttonPrevious;

        private RectangleButton buttonNext;

        private SynchronizationSession synchronizationSession;

        private TestSession testSession;

        private OperationSession operationSession;

        private FsScriptSearcher scriptSearcher;

        private FsScriptRunner scriptRunner;

        public MainScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.IsPopup = true;
        }

        public MMCircularLayer CircularLayers { get; private set; }

        #region Load and Unload

        public override void LoadContent(IMMEngineInteropService interop)
        {
            // Script runner
            this.scriptSearcher = new FsScriptSearcher();
            this.scriptRunner   = new FsScriptRunner(this.scriptSearcher, Unity.FsiSession);
            this.scriptRunner.Search();

            // Synchronization session
            this.synchronizationSession = new SynchronizationSession();

            // Test session
            this.testSession = new TestSession(Unity.FsiSession, Unity.SessionData.Cognition);

            // Operation session
            this.operationSession = new OperationSession(Unity.FsiSession, Unity.SessionData.Cognition);

            var graphicsSettings = this.Graphics.Settings;

            // Buttons
            const int buttonWidth = 30;
            const int buttonHeight = 300;
            var buttonY = (graphicsSettings.Height - buttonHeight) / 2;
            var buttonSettings = new RectangleButtonSettings
            {
                BoundaryRegularColor      = Palette.Transparent,
                BoundaryMouseOverColor    = Palette.Transparent,
                BoundaryMousePressColor = Palette.Transparent,
                FillMouseOverColor            = Palette.Transparent20,
                FillMousePressColor         = Palette.Transparent40
            };

            this.buttonPrevious = new RectangleButton(
                new Rectangle(0, buttonY, buttonWidth, buttonHeight), buttonSettings)
            {
                Label =
                {
                    TextFont   = () => Font.UiRegular,
                    Text       = () => "<",
                    TextColor  = () => Color.White,
                    TextSize   = () => 1f,
                },
            };
            this.buttonPrevious.MousePressLeft +=
                (sender, args) => this.CircularLayers.Previous();

            this.buttonNext = new RectangleButton(
                new Rectangle(graphicsSettings.Width - buttonWidth, buttonY, buttonWidth, buttonHeight), buttonSettings)
            {
                Label =
                {
                    TextFont   = () => Font.UiRegular,
                    Text       = () => ">",
                    TextColor  = () => Color.White,
                    TextSize   = () => 1f,
                }
            };
            this.buttonNext.MousePressLeft +=
                (sender, args) => this.CircularLayers.Next();

            this.Layers.Add(new SynchronizationLayer(this));
            this.Layers.Add(new MMLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    // Buttons have the same alpha value as circular layer
                    SpriteBatch.Begin();
                    this.buttonPrevious.Draw(
                        graphics,
                        time,
                        Math.Min(
                            this.TransitionOpacity,
                            this.CircularLayers.Opacity));
                    this.buttonNext.Draw(
                        graphics,
                        time,
                        Math.Min(
                            this.TransitionOpacity,
                            this.CircularLayers.Opacity));
                    SpriteBatch.End();
                }
            });

            this.CircularLayers = new MMCircularLayer(this);
            this.CircularLayers.Add(new TestLayer(this.testSession, Unity.Speech, this));
            this.CircularLayers.Add(new OperationLayer(this.operationSession, this)
            {
                Active = false,
                Visible = false,
                Opacity = 0,
            });

            this.Layers.Add(this.CircularLayers);

            // First run of fsi session
            this.RunScripts();

            base.LoadContent(interop);
        }

        #endregion

        public override void Update(GameTime time)
        {
            this.buttonPrevious.Update(time);
            this.buttonNext.Update(time);

            base.Update(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.SessionRerun))
            {
                this.RunScripts();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.SynchronizationPause))
            {
                this.synchronizationSession.ToggleSynchronization();
            }

            this.buttonPrevious.UpdateInput(input, time);
            this.buttonNext.UpdateInput(input, time);

            base.UpdateInput(input, time);
        }

        private void RunScripts()
        {
            Unity.SessionData.Operation.Reset();
            Unity.SessionData.Test     .Reset();

            this.scriptRunner.Rerun();
        }
    }
}