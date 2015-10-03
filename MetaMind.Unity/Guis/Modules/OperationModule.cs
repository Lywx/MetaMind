namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine;
    using Engine.Entities;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Views;
    using Engine.Gui.Modules;
    using Engine.Service;
    using Microsoft.Xna.Framework;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Operations;

    public class OperationModule : MMMvcEntity<OperationModuleSettings>
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

            this.Entities = new MMEntityCollection<IMMViewNode>();
        }

        private MMEntityCollection<IMMViewNode> Entities { get; set; }

        private IMMViewNode View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IMMEngineInteropService interop)
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
            this.View = new MMViewNode(viewSettings, itemSettings, new List<IViewItem>());

            var viewCompositor = new OperationIndexViewBuilder(this.operationSession);
            viewCompositor.Compose(this.View, this.operation);

            // Entities
            this.Entities.Add(this.View);
            this.Entities.LoadContent(interop);

            base.LoadContent(interop);
        }

        #endregion

        #region Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Entities.Draw(graphics, time, alpha);
            base         .Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
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