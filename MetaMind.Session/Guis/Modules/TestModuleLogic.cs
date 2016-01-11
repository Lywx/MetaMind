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
    using Tests;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Tests;

    public class TesTMVCController : MMMVCEntityController<TestModule, TesTMVCSettings, TesTMVCController>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        public TesTMVCController(TestModule module, ITest test, TestSession testSession) 
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

            this.TestMonitor = new TestMonitor(this.GlobalInterop.Engine, this.test);

            this.ControllableEntities = new MMEntityCollection<IMMInputEntity>();
        }

        public MMEntityCollection<IMMInputEntity> ControllableEntities { get; set; }

        public TestMonitor TestMonitor { get; set; }

        public IMMView View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent()
        {
            var graphicsSettings = this.EngineGraphics.Settings;

            // View settings
            var viewSettings = new StandardIndexViewSettings(
                itemMargin    : new Vector2(graphicsSettings.Width - TesTMVCSettings.ViewMargin.X * 2, TesTMVCSettings.ItemMargin.Y),
                viewPosition  : TesTMVCSettings.ViewMargin.ToVector2(),
                viewRowDisplay: (graphicsSettings.Height - TesTMVCSettings.ViewMargin.Y) / TesTMVCSettings.ItemMargin.Y - 1,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new TestItemSettings();

            // View construction
            this.View = new MMView(viewSettings, itemSettings, new List<IMMViewItem>());

            var viewCompositor = new TestIndexViewBuilder(this.testSession);
            viewCompositor.Compose(this.View, this.test);

            // Entities
            this.ControllableEntities.Add(this.View);
            this.ControllableEntities.LoadContent();
            base.LoadContent();
        }

        #endregion

        public override void UpdateInput(GameTime time)
        {
            this.ControllableEntities.UpdateInput(time);
            base                     .UpdateInput(time);
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