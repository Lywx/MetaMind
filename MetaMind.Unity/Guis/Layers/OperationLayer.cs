namespace MetaMind.Unity.Guis.Layers
{
    using System;
    using Concepts.Operations;
    using Engine;
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Modules;

    public class OperationLayer : GameLayer
    {
        private readonly OperationSession operationSession;

        public OperationLayer(OperationSession operationSession, IGameScreen screen, byte transitionAlpha = byte.MaxValue)
            : base(screen, transitionAlpha)
        {
            if (operationSession == null)
            {
                throw new ArgumentNullException(nameof(operationSession));
            }

            this.operationSession = operationSession;

            this.ControllableEntities = new GameControllableEntityCollection<IGameControllableEntity>();
            this.VisuallEntities      = new GameVisualEntityCollection<IGameVisualEntity>();
        }

        private GameVisualEntityCollection<IGameVisualEntity> VisuallEntities { get; set; }

        private GameControllableEntityCollection<IGameControllableEntity> ControllableEntities { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            graphics.SpriteBatch.Begin();

            this.ControllableEntities.Draw(graphics, time, Math.Min(alpha, this.TransitionAlpha));
            this.VisuallEntities     .Draw(graphics, time, Math.Min(alpha, this.TransitionAlpha));

            graphics.SpriteBatch.End();

            base.Draw(graphics, time, alpha);
        }

        public override void LoadContent(IGameInteropService interop)
        {
            var operation = Unity.SessionData.Operation;

            var operationModule = new OperationModule(new OperationModuleSettings(),
                operation, this.operationSession);

            this.ControllableEntities.Add(operationModule);
            this.ControllableEntities.LoadContent(interop);

            var graphicsSettings = this.Graphics.Settings;
            var screenLabel = new Label
            {
                TextFont     = () => Font.UiRegular,
                Text         = () => "OPERATIONS",
                TextPosition = () => new Vector2((float)graphicsSettings.Width / 2, 80),
                TextColor    = () => Color.White,
                TextSize     = () => 1.0f,
                TextHAlign   = StringHAlign.Center,
                TextVAlign   = StringVAlign.Center,
            };

            this.VisuallEntities.Add(screenLabel);
            this.VisuallEntities.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.ControllableEntities.UnloadContent(interop);
            this.VisuallEntities     .UnloadContent(interop);
            base                     .UnloadContent(interop);
        }

        public override void Update(GameTime time)
        {
            this.ControllableEntities.Update(time);
            this.VisuallEntities     .Update(time);
            base                     .Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.ControllableEntities.UpdateInput(input, time);
            base.                     UpdateInput(input, time);
        }
    }
}