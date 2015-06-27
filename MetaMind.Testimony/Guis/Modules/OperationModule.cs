namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Scripting;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Operations;
    using Widgets.IndexViews.Tests;

    public class OperationModule : Module<OperationModuleSettings>
    {
        private readonly IOperationDescription operations;

        private readonly OperationSession operationSession;

        public OperationModule(OperationModuleSettings settings, IOperationDescription operations, FsiSession fsiSession)
            : base(settings)
        {
            if (operations == null)
            {
                throw new ArgumentNullException("operations");
            }

            if (fsiSession == null)
            {
                throw new ArgumentNullException("fsiSession");
            }

            this.operations       = operations;
            this.operationSession = new OperationSession(fsiSession);
            Operation.Session     = operationSession;

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
                viewRowDisplay: 28,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new TestItemSettings();
            var statusFrameSettings = itemSettings.Get<FrameSettings>("StatusFrame");
            statusFrameSettings.Size = new Point(128, 52);

            // View construction
            this.View = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewComposer = new OperationIndexViewComposer(operationSession);
            viewComposer.Compose(this.View, this.operations);

            // Entities
            this.Entities.Add(this.View);
            this.Entities.LoadContent(interop);

            base.LoadContent(interop);
        }

        #endregion

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Entities.Draw(graphics, time, alpha);

            base.Draw(graphics, time, alpha);
        }

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