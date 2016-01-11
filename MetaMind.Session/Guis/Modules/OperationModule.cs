namespace MetaMind.Session.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Engine.Entities;
    using Engine.Entities.Bases;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Operations;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Operations;

    public class OperationModule : MMMVCEntity<OperationModuleSettings>
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

            this.Entities = new MMEntityCollection<IMMView>();
        }

        private MMEntityCollection<IMMView> Entities { get; set; }

        private IMMView View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent()
        {
            var graphicsSettings = this.EngineGraphics.Settings;

            // View settings
            var viewSettings = new StandardIndexViewSettings(
                itemMargin    : new Vector2(graphicsSettings.Width - OperationModuleSettings.ViewMargin.X * 2, OperationModuleSettings.ItemMargin.Y),
                viewPosition  : OperationModuleSettings.ViewMargin.ToVector2(),
                viewRowDisplay: (graphicsSettings.Height - OperationModuleSettings.ViewMargin.Y) / OperationModuleSettings.ItemMargin.Y - 1,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new OperationItemSettings();

            // View construction
            this.View = new MMView(viewSettings, itemSettings, new List<IMMViewItem>());

            var viewCompositor = new OperationIndexViewBuilder(this.operationSession);
            viewCompositor.Compose(this.View, this.operation);

            // Entities
            this.Entities.Add(this.View);
            this.Entities.LoadContent();

            base.LoadContent();
        }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            this.Entities.Draw(time);
            base         .Draw(time);
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            this.Entities.UpdateInput(time);
            base         .UpdateInput(time);
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