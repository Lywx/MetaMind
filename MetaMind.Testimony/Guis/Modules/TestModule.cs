namespace MetaMind.Testimony.Guis.Modules
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    using Concepts.Tests;
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Factories;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layers;
    using Engine.Guis.Widgets.Regions;
    using Engine.Guis.Widgets.Views;
    using Engine.Guis.Widgets.Views.Layouts;
    using Engine.Guis.Widgets.Views.Regions;
    using Engine.Guis.Widgets.Views.Scrolls;
    using Engine.Guis.Widgets.Views.Selections;
    using Engine.Guis.Widgets.Views.Swaps;
    using Engine.Guis.Widgets.Views.Visuals;
    using Engine.Services;
    using Widgets;

    public class TestModule : Module<TestModuleSettings>
    {
        private readonly GameControllableEntityCollection<IView> entities = new GameControllableEntityCollection<IView>();

        private readonly ITest test;

        private IView view;

        public TestModule(ITest test, TestModuleSettings settings)
            : base(settings)
        {
            this.test = test;

            this.Logic  = new TestModuleLogic(this);
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

        public ITest Test
        {
            get { return this.test; }
        }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IGameInteropService interop)
        {
            // View settings
            var viewSettings = new TestViewSettings(
                itemMargin    : new Vector2(512 + 128 + 24, 26),
                viewPosition  : new Vector2(40, 100),
                viewRowDisplay: 30,
                viewRowMax    : 100);

            // Item settings
            var itemSettings = new TestItemSettings();

            // View construction
            this.view = new View(viewSettings, itemSettings, new List<IViewItem>());

            // View composition
            var viewSelection = new BlockViewVerticalSelectionController(this.View);
            var viewScroll    = new BlockViewVerticalScrollController(this.View);
            var viewSwap      = new ViewSwapController(this.View);
            var viewLayout    = new BlockViewVerticalLayout(this.View);
            var viewLayer     = new BlockViewVerticalLayer(this.View);

            // Item composition
            var itemFactory = new ViewItemFactory(

                item => new TestItemLayer(item),

                item =>
                {
                    var itemFrame             = new TestItemFrame(item);
                    var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(item, viewSelection, viewScroll);
                    var itemLayout            = new TestItemLayout(item, itemLayoutInteraction);
                    var itemInteraction       = new BlockViewVerticalItemInteraction(item, itemLayout, itemLayoutInteraction);
                    var itemModel             = new ViewItemDataModel(item);

                    return new TestItemLogic(
                        item,
                        itemFrame,
                        itemInteraction,
                        itemModel,
                        itemLayout);
                },

                item => new TestItemVisual(item));

            // View logic
            var viewLogic  = new TestViewLogic(this.View, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory);
            viewLogic.ViewBinding = new TestViewBinding(viewLogic, this.Test.Children);

            // View visual
            var viewVisual = new GradientViewVisual(this.View);

            // View setup
            this.View.ViewLayer = viewLayer;
            this.View.ViewLogic = viewLogic;
            this.View.ViewVisual = viewVisual;

            // View region
            var viewRegionSettings = new ViewRegionSettings();
            var viewRegion = new ViewRegion(
                regionBounds: () => new Rectangle(
                    location: viewSettings.ViewPosition.ToPoint(),
                    size: new Point(
                        x: 512 + 128 + 24,
                        y: (int)(viewSettings.ViewRowDisplay * viewSettings.ItemMargin.Y))),
                regionSettings: viewRegionSettings);
            this.View.ViewComponents.Add("ViewRegion", viewRegion);

            // View scrollbar
            var viewScrollbar = this.SetupViewScrollbar(this.View, viewSettings, viewLayer, viewScroll, viewLayout, viewRegion);

            // View focus
            this.View[ViewState.View_Has_Focus] = () =>
                this.View[ViewState.View_Has_Selection]() ||
                viewRegion[RegionState.Region_Has_Focus]() ||
                viewScrollbar[FrameState.Frame_Is_Dragging]();

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

        #region Composition

        private IViewVerticalScrollbar SetupViewScrollbar(
            IView view,
            TestViewSettings viewSettings,
            BlockViewVerticalLayer viewLayer,
            BlockViewVerticalScrollController viewScroll,
            BlockViewVerticalLayout viewLayout,
            ViewRegion viewRegion)
        {
            var viewVerticalScrollbarSettings = viewSettings.Get<ViewScrollbarSettings>("ViewVerticalScrollbar");

            var viewScrollbar = new ViewVerticalScrollbar(viewSettings, viewScroll, viewLayout, viewRegion, viewVerticalScrollbarSettings);

            view.ViewComponents.Add("ViewVerticalScrollbar", viewScrollbar);

            viewLayer.ViewLogic.ScrolledUp   += (sender, args) => viewScrollbar.Toggle();
            viewLayer.ViewLogic.ScrolledDown += (sender, args) => viewScrollbar.Toggle();
            viewLayer.ViewLogic.MovedUp      += (sender, args) => viewScrollbar.Toggle();
            viewLayer.ViewLogic.MovedDown    += (sender, args) => viewScrollbar.Toggle();

            return viewScrollbar;
        }

        #endregion
    }
}
