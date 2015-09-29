namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
    using Engine;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Views;
    using Engine.Gui.Modules;
    using Engine.Service;
    using Microsoft.Xna.Framework;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Tests;

    public class TestModuleLogic : GameEntityModuleLogic<TestModule, TestModuleSettings, TestModuleLogic>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        public TestModuleLogic(TestModule module, ITest test, TestSession testSession) 
            : base(module)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            if (testSession == null)
            {
                throw new ArgumentNullException(nameof(testSession));
            }

            this.test        = test;
            this.testSession = testSession;

            this.TestMonitor = new TestMonitor(this.Interop.Engine, this.test);

            this.ControllableEntities = new GameEntityCollection<IGameInputableEntity>();
        }

        public GameEntityCollection<IGameInputableEntity> ControllableEntities { get; set; }

        public TestMonitor TestMonitor { get; set; }

        public IView View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IGameInteropService interop)
        {
            var graphicsSettings = this.Graphics.Settings;

            // View settings
            var viewSettings = new StandardIndexViewSettings(
                itemMargin    : new Vector2(graphicsSettings.Width - TestModuleSettings.ViewMargin.X * 2, TestModuleSettings.ItemMargin.Y),
                viewPosition  : TestModuleSettings.ViewMargin.ToVector2(),
                viewRowDisplay: (graphicsSettings.Height - TestModuleSettings.ViewMargin.Y) / TestModuleSettings.ItemMargin.Y - 1,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new TestItemSettings();

            // View construction
            this.View = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewCompositor = new TestIndexViewBuilder(this.testSession);
            viewCompositor.Compose(this.View, this.test);

            // Entities
            this.ControllableEntities.Add(this.View);
            this.ControllableEntities.LoadContent(interop);
            base.LoadContent(interop);
        }

        #endregion

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.ControllableEntities.UpdateInput(input, time);
            base                     .UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.ControllableEntities.UpdateForwardBuffer();
            this.ControllableEntities.Update(time);
            this.ControllableEntities.UpdateBackwardBuffer();
            base                     .Update(time);
        }
    }
}