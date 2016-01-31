namespace MetaMind.Session.Guis.Layers
{
    using System;
    using Engine.Core.Backend.Content.Fonts;
    using Engine.Core.Entity.Common;
    using Engine.Core.Entity.Control.Labels;
    using Engine.Core.Entity.Graphics.Fonts;
    using Engine.Core.Entity.Screens;
    using Microsoft.Xna.Framework;
    using Modules;
    using Operations;

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

            this.ControllableEntities = new MMEntityCollection<IMMInputtableEntity>();
            this.VisuallEntities      = new MMEntityCollection<IMMVisualEntity>();
        }

        private MMEntityCollection<IMMVisualEntity> VisuallEntities { get; set; }

        private MMEntityCollection<IMMInputtableEntity> ControllableEntities { get; set; }

        public override void Draw(GameTime time)
        {
            base.Draw(time);

            ((MMVisualEntity)this).EngineGraphics.Renderer.Begin();

            this.ControllableEntities.Draw(time);
            this.VisuallEntities     .Draw(time);
            
            ((MMVisualEntity)this).EngineGraphics.Renderer.End();
        }

        public override void LoadContent()
        {
            var operation = MMSessionGame.SessionData.Operation;

            var operationModule = new OperationModule(new OperationModuleSettings(),
                operation, this.operationSession);

            this.ControllableEntities.Add(operationModule);
            this.ControllableEntities.LoadContent();

            var graphicsSettings = this.EngineGraphics.Settings;
            var screenLabel = new Label
            {
                TextFont     = () => MMFont.UiRegular,
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