namespace MetaMind.Session.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
    using Engine.Entities;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Widgets.IndexViews;
    using Widgets.IndexViews.Tests;

    public class TesTMvcLogic : MMMvcEntityLogic<TestModule, TesTMvcSettings, TesTMvcLogic>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        public TesTMvcLogic(TestModule module, ITest test, TestSession testSession) 
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

            this.ControllableEntities = new MMEntityCollection<IMMInputEntity>();
        }

        public MMEntityCollection<IMMInputEntity> ControllableEntities { get; set; }

        public TestMonitor TestMonitor { get; set; }

        public IMMViewNode View { get; set; }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IMMEngineInteropService interop)
        {
            var graphicsSettings = this.Graphics.Settings;

            // View settings
            var viewSettings = new StandardIndexViewSettings(
                itemMargin    : new Vector2(graphicsSettings.Width - TesTMvcSettings.ViewMargin.X * 2, TesTMvcSettings.ItemMargin.Y),
                viewPosition  : TesTMvcSettings.ViewMargin.ToVector2(),
                viewRowDisplay: (graphicsSettings.Height - TesTMvcSettings.ViewMargin.Y) / TesTMvcSettings.ItemMargin.Y - 1,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new TestItemSettings();

            // View construction
            this.View = new MMViewNode(viewSettings, itemSettings, new List<IViewItem>());

            var viewCompositor = new TestIndexViewBuilder(this.testSession);
            viewCompositor.Compose(this.View, this.test);

            // Entities
            this.ControllableEntities.Add(this.View);
            this.ControllableEntities.LoadContent(interop);
            base.LoadContent(interop);
        }

        #endregion

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
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