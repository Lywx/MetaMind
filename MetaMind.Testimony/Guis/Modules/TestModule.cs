namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Scripting;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Tests;

    public class TestModule : Module<TestModuleSettings>
    {
        private readonly GameControllableEntityCollection<IView> entities = new GameControllableEntityCollection<IView>();

        private readonly ITest test;

        private readonly TestSession testSession;

        private IView view;

        public TestModule(TestModuleSettings settings, ITest test, FsiSession fsiSession)
            : base(settings)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            if (fsiSession == null)
            {
                throw new ArgumentNullException("fsiSession");
            }

            this.test        = test;
            this.testSession = new TestSession(fsiSession);
            Test.TestSession = this.testSession;

            this.Logic  = new TestModuleLogic(this, this.test, this.testSession);
            this.Visual = new TestModuleVisual(this);
        }

        public GameControllableEntityCollection<IView> Entities
        {
            get { return this.entities; }
        }

        public IView View
        {
            get { return this.view; }
        }

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
            var itemSettings = new TestIndexItemSettings();

            // View construction
            this.view = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewComposer = new TestIndexViewComposer(this.testSession);
            viewComposer.Compose(this.view, this.test);

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
