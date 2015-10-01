namespace MetaMind.Unity.Guis.Layers
{
    using System;
    using Concepts.Operations;
    using Engine;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics.Fonts;
    using Engine.Entities;
    using Engine.Gui.Controls.Labels;
    using Engine.Screen;
    using Engine.Service;
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

            this.ControllableEntities = new MMEntityCollection<IMMInputableEntity>();
            this.VisuallEntities      = new MMEntityCollection<IMMVisualEntity>();
        }

        private MMEntityCollection<IMMVisualEntity> VisuallEntities { get; set; }

        private MMEntityCollection<IMMInputableEntity> ControllableEntities { get; set; }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);

            this.SpriteBatch.Begin();

            this.ControllableEntities.Draw(graphics, time, Math.Min(alpha, this.Opacity));
            this.VisuallEntities     .Draw(graphics, time, Math.Min(alpha, this.Opacity));
            
            this.SpriteBatch.End();
        }

        public override void LoadContent(IMMEngineInteropService interop)
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
                AnchorLocation = () => new Vector2((float)graphicsSettings.Width / 2, 80),
                TextColor    = () => Color.White,
                TextSize     = () => 1.0f,
                TextHAlignment   = HoritonalAlignment.Center,
                TextVAlignment   = VerticalAlignment.Center,
            };

            this.VisuallEntities.Add(screenLabel);
            this.VisuallEntities.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IMMEngineInteropService interop)
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

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.ControllableEntities.UpdateInput(input, time);
            base.                     UpdateInput(input, time);
        }
    }
}