namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Scripting;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Operations;

    public class OperationModule : Module<TestModuleSettings>
    {
        private List<IOperation> operations;

        private OperationSession operationSession;

        public OperationModule(TestModuleSettings settings, List<IOperation> operations, OperationSession operationSession)
            : base(settings)
        {
            if (operations == null)
            {
                throw new ArgumentNullException("operations");
            }

            if (operationSession == null)
            {
                throw new ArgumentNullException("operationSession");
            }

            this.operations       = operations;
            this.operationSession = operationSession;

            this.Entities = new GameControllableEntityCollection<IView>();
        }

        private GameControllableEntityCollection<IView> Entities { get; set; }

        private IView View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IGameInteropService interop)
        {
            // View settings
            var viewSettings = new StandardIndexViewSettings(
                itemMargin    : new Vector2(1355 + 128 + 24, 26),
                viewPosition  : new Vector2(40, 100),
                viewRowDisplay: 30,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new StandardIndexItemSettings();

            // View construction
            this.View = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewComposer = new OperationIndexViewComposer(new OperationSession(Testimony.FsiSession));
            viewComposer.Compose(this.View, this.operations);

            // Entities
            this.Entities.Add(this.View);
            this.Entities.LoadContent(interop);

            base.LoadContent(interop);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);

            base.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.Entities.UpdateForwardBuffer();
            this.Entities.Update(time);
            this.Entities.UpdateBackwardBuffer();

            base.Update(time);
        }

        #endregion
    }
}