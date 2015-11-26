namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Concepts.Operations;
    using Concepts.Synchronizations;
    using Concepts.Tests;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics;
    using Engine.Entities;
    using Engine.Entities.Controls.Buttons;
    using Engine.Screens;
    using Engine.Services;
    using Engine.Services.Script.FSharp;
    using Engine.Settings;
    using Layers;
    using Microsoft.Xna.Framework;

    public class MainScreen : MMScreen
    {
        private MMRectangleButton buttonPrevious;

        private MMRectangleButton buttonNext;

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

        public override void LoadContent()
        {
            // Script runner
            this.scriptSearcher = new FsScriptSearcher();
            this.scriptRunner   = new FsScriptRunner(this.scriptSearcher, SessionGame.FsiSession);
            this.scriptRunner.Search();

            // Synchronization session
            this.synchronizationSession = new SynchronizationSession();

            // Test session
            this.testSession = new TestSession(SessionGame.FsiSession, SessionGame.SessionData.Cognition);

            // Operation session
            this.operationSession = new OperationSession(SessionGame.FsiSession, SessionGame.SessionData.Cognition);

            var graphicsSettings = this.Graphics.Settings;

            // Buttons
            const int buttonWidth = 30;
            const int buttonHeight = 300;
            var buttonY = (graphicsSettings.Height - buttonHeight) / 2;
            var buttonSettings = new MMRectangleButtonSettings
            {
                BoundaryRegularColor      = MMPalette.Transparent,
                BoundaryMouseOverColor    = MMPalette.Transparent,
                BoundaryMousePressColor = MMPalette.Transparent,
                FillMouseOverColor            = MMPalette.Transparent20,
                FillMousePressColor         = MMPalette.Transparent40
            };

            this.buttonPrevious = new MMRectangleButton(
                new Rectangle(0, buttonY, buttonWidth, buttonHeight), buttonSettings)
            {
                Label =
                {
                    TextFont   = () => Font.UiRegular,
                    Text       = () => "<",
                    TextColor  = () => this.Color.White,
                    TextSize   = () => 1f,
                },
            };
            this.buttonPrevious.MousePressLeft +=
                (sender, args) => this.CircularLayers.Previous();

            this.buttonNext = new MMRectangleButton(
                new Rectangle(graphicsSettings.Width - buttonWidth, buttonY, buttonWidth, buttonHeight), buttonSettings)
            {
                Label =
                {
                    TextFont   = () => Font.UiRegular,
                    Text       = () => ">",
                    TextColor  = () => this.Color.White,
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
                    ((MMVisualEntity)this).Graphics.Renderer.Begin();
                    this.buttonPrevious.Draw(time);
                    this.buttonNext.Draw(time);
                    ((MMVisualEntity)this).Graphics.Renderer.End();
                }
            });

            this.CircularLayers = new MMCircularLayer(this);
            this.CircularLayers.Add(new TestLayer(this.testSession, SessionGame.Speech, this));
            this.CircularLayers.Add(new OperationLayer(this.operationSession, this)
            {
                Enabled = false,
                Visible = false,
                Opacity = 0,
            });

            this.Layers.Add(this.CircularLayers);

            // First run of fsi session
            this.RunScripts();

            base.LoadContent();
        }

        #endregion

        public override void Update(GameTime time)
        {
            this.buttonPrevious.Update(time);
            this.buttonNext.Update(time);

            base.Update(time);
        }

        public override void UpdateInput(GameTime time)
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

            this.buttonPrevious.UpdateInput(time);
            this.buttonNext.UpdateInput(time);

            base.UpdateInput(time);
        }

        private void RunScripts()
        {
            SessionGame.SessionData.Operation.Reset();
            SessionGame.SessionData.Test     .Reset();

            this.scriptRunner.Rerun();
        }
    }
}