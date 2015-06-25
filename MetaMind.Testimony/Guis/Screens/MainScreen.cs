namespace MetaMind.Testimony.Guis.Screens
{
    using System;
    using Engine.Components.Fonts;
    using Engine.Guis.Layers;
    using Engine.Guis.Widgets.Buttons;
    using Engine.Screens;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Layers;
    using Microsoft.Xna.Framework;

    public class MainScreen : GameScreen
    {
        private Button buttonPrevious;

        private Button buttonNext;

        public MainScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.IsPopup = true;
        }

        private CircularGameLayer CircularLayers { get; set; }

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
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

            this.Layers.Add(new SynchronizationLayer(this));

            this.CircularLayers = new CircularGameLayer(this);
            this.CircularLayers.Add(new TestLayer(this));
            this.CircularLayers.Add(new OperationLayer(this));
            this.Layers.Add(this.CircularLayers);

            base.LoadContent(interop);
        }

        #endregion

        public override void Update(GameTime time)
        {
            this.buttonPrevious.Update(time);
            this.buttonNext    .Update(time);

            base.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.buttonPrevious.UpdateInput(input, time);
            this.buttonNext    .UpdateInput(input, time);

            base.UpdateInput(input, time);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            graphics.SpriteBatch.Begin();

            this.buttonPrevious.Draw(graphics, time, this.TransitionAlpha);
            this.buttonNext    .Draw(graphics, time, this.TransitionAlpha);

            graphics.SpriteBatch.End();

            base.Draw(graphics, time);
        }
    }
}