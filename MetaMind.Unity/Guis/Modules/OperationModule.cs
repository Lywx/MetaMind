namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Operations;

    public class OperationModule : GameEntityModule<OperationModuleSettings>
    {
        private readonly IOperationDescription operation;

        private readonly OperationSession operationSession;

        public OperationModule(OperationModuleSettings settings, IOperationDescription operation, OperationSession operationSession)
            : base(settings)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (operationSession == null)
            {
                throw new ArgumentNullException(nameof(operationSession));
            }

            this.operation       = operation;
            this.operationSession = operationSession;
            Operation.Session     = operationSession;

            this.Entities = new GameControllableEntityCollection<IView>();
        }

        private GameControllableEntityCollection<IView> Entities { get; set; }

        private IView View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IGameInteropService interop)
        {
            var graphicsSettings = this.Graphics.Settings;

            // View settings
            var viewSettings = new StandardIndexViewSettings(
                itemMargin    : new Vector2(graphicsSettings.Width - OperationModuleSettings.ViewMargin.X * 2, OperationModuleSettings.ItemMargin.Y),
                viewPosition  : OperationModuleSettings.ViewMargin.ToVector2(),
                viewRowDisplay: (graphicsSettings.Height - OperationModuleSettings.ViewMargin.Y) / OperationModuleSettings.ItemMargin.Y - 1,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new OperationItemSettings();

            // View construction
            this.View = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewCompositor = new OperationIndexViewCompositor(this.operationSession);
            viewCompositor.Compose(this.View, this.operation);

            // Entities
            this.Entities.Add(this.View);
            this.Entities.LoadContent(interop);

            base.LoadContent(interop);
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Entities.Draw(graphics, time, alpha);
            base         .Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);
            base         .UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.Entities.UpdateForwardBuffer();
            this.Entities.Update(time);
            this.Entities.UpdateBackwardBuffer();
            base         .Update(time);
        }

        #endregion
    }
}