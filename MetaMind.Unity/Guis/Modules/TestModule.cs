namespace MetaMind.Unity.Guis.Modules
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
    using Widgets.IndexViews;
    using Widgets.IndexViews.Tests;

    public class TestModule : Module<TestModuleSettings>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        public TestModule(TestModuleSettings settings, ITest test, TestSession testSession)
            : base(settings)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            if (testSession == null)
            {
                throw new ArgumentNullException("testSession");
            }

            this.test        = test;
            this.testSession = testSession;
            Test.Session = this.testSession;

            this.Entities = new GameControllableEntityCollection<IView>();
        }

        private IView View { get; set; }

        private GameControllableEntityCollection<IView> Entities { get; set; }

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
            var itemSettings = new TestItemSettings();

            // View construction
            this.View = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewComposer = new TestIndexViewComposer(this.testSession);
            viewComposer.Compose(this.View, this.test);

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
    }
}
