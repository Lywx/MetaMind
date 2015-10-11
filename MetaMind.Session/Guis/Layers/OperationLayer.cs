namespace MetaMind.Session.Guis.Layers
{
    using System;
    using Concepts.Operations;
    using Engine.Components.Content.Fonts;
    using Engine.Entities;
    using Engine.Entities.Controls.Labels;
    using Engine.Entities.Graphics.Fonts;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Modules;

    // TODO(Critical): I need to change this. I don't need this anymore
    public class OperationLayer : MMLayer
    {
        private readonly OperationSession operationSession;

        public OperationLayer(OperationSession operationSession, IMMScreen screen)
            : base(screen)
        {
            if (operationSession == null)
            {
                throw new ArgumentNullException(nameof(operationSession));
            }

            this.operationSession = operationSession;

            this.ControllableEntities = new MMEntityCollection<IMMInputEntity>();
            this.VisuallEntities      = new MMEntityCollection<IMMVisualEntity>();
        }

        private MMEntityCollection<IMMVisualEntity> VisuallEntities { get; set; }

        private MMEntityCollection<IMMInputEntity> ControllableEntities { get; set; }

        public override void Draw(GameTime time)
        {
            base.Draw(time);

            this.SpriteBatch.Begin();

            this.ControllableEntities.Draw(time);
            this.VisuallEntities     .Draw(time);
            
            this.SpriteBatch.End();
        }

        public override void LoadContent()
        {
            var operation = SessionGame.SessionData.Operation;

            var operationModule = new OperationModule(new OperationModuleSettings(),
                operation, this.operationSession);

            this.ControllableEntities.Add(operationModule);
            this.ControllableEntities.LoadContent();

            var graphicsSettings = this.Graphics.Settings;
            var screenLabel = new Label
            {
                TextFont     = () => Font.UiRegular,
                Text         = () => "OPERATIONS",
                AnchorLocation = () => new Vector2((float)graphicsSettings.Width / 2, 80),
                TextColor    = () => Color.White,
                TextSize     = () => 1.0f,
                TextHAlignment   = HoritonalAlignment.Center,
                TextVAlignment   = VerticalAlignment.Center,
            };

            this.VisuallEntities.Add(screenLabel);
            this.VisuallEntities.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            this.ControllableEntities.UnloadContent();
            this.VisuallEntities     .UnloadContent();
            base                     .UnloadContent();
        }

        public override void Update(GameTime time)
        {
            this.ControllableEntities.Update(time);
            this.VisuallEntities     .Update(time);
            base                     .Update(time);
        }

        public override void UpdateInput(GameTime time)
        {
            this.ControllableEntities.UpdateInput(time);
            base.                     UpdateInput(time);
        }
    }
}