namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
    using Engine;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Item.Factories;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Item.Layers;
    using Engine.Gui.Controls.Regions;
    using Engine.Gui.Controls.Views;
    using Engine.Gui.Controls.Views.Layouts;
    using Engine.Gui.Controls.Views.Logic;
    using Engine.Gui.Controls.Views.Regions;
    using Engine.Gui.Controls.Views.Scrolls;
    using Engine.Gui.Controls.Views.Selections;
    using Engine.Gui.Controls.Views.Swaps;
    using Engine.Gui.Controls.Views.Visuals;
    using Engine.Gui.Elements;
    using Microsoft.Xna.Framework;
    using Modules;

    /// <summary>
    /// Composers are not intended to be reused.
    /// </summary>
    public class TestIndexViewBuilder : GameVisualEntity, IIndexViewBuilder
    {
        private StandardIndexViewSettings viewSettings;

        private IViewLogic viewLogic;

        private IViewVisual viewVisual;

        private BlockViewVerticalLayer viewLayer;

        private IViewVerticalScrollbar viewScrollbar;

        public TestIndexViewBuilder(TestSession testSeesion)
        {
            if (testSeesion == null)
            {
                throw new ArgumentNullException(nameof(testSeesion));
            }

            this.TestSession = testSeesion;
        }

        protected IView View { get; set; }

        protected BlockViewVerticalScrollController ViewScroll { get; set; }

        protected BlockViewVerticalSelectionController ViewSelection { get; set; }

        protected IndexBlockViewVerticalLayout ViewLayout { get; set; }

        protected ViewSwapController ViewSwap { get; set; }

        protected ViewRegion ViewRegion { get; set; }

        protected ViewItemFactory ItemFactory { get; set; }

        protected dynamic ViewData { get; set; }

        protected TestSession TestSession { get; set; }

        public virtual void Compose(IView view, dynamic viewData)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (viewData == null)
            {
                throw new ArgumentNullException(nameof(viewData));
            }

            this.View     = view;
            this.ViewData = viewData;

            this.AddView();
            this.AddViewRegion();
            this.AddViewScrollbar();

            this.SetupLogic();
        }

        public IView Clone(IViewItem item)
        {
            var itemLayer  = item.GetLayer<BlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLayout;

            var hostViewLayer = item.View.GetLayer<BlockViewVerticalLayer>();
            var hostViewSettings = hostViewLayer.ViewSettings;
            var hostViewScroll   = hostViewLayer.ViewScroll;

            var indexViewSettings = (StandardIndexViewSettings)item.View.ViewSettings.Clone();
            var indexItemSettings = (TestItemSettings)item.View.ItemSettings.Clone();

            indexViewSettings.ViewRowDisplay = hostViewSettings.ViewRowDisplay - itemLayout.Row - itemLayout.BlockRow;
            indexViewSettings.ViewPosition   = hostViewScroll.Position(itemLayout.Row + itemLayout.BlockRow);

            return new View(indexViewSettings, indexItemSettings, new List<IViewItem>());
        }

        protected void AddView()
        {
            var test = (Test)this.ViewData;

            this.viewSettings = (StandardIndexViewSettings)this.View.ViewSettings;

            // View composition
            this.ViewSelection = this.AddViewSelection();
            this.ViewScroll    = new IndexBlockViewVerticalScrollController(this.View);
            this.ViewSwap      = new ViewSwapController(this.View);
            this.ViewLayout    = new IndexBlockViewVerticalLayout(this.View);
            this.viewLayer     = new BlockViewVerticalLayer(this.View);

            // Item composition
            this.AddItemFactory();

            // View logic
            this.viewLogic = this.AddViewLogic();
            this.viewLogic.ViewBinding = new TestViewBinding(
                this.viewLogic,
                test.Children,
                this.TestSession);

            // View visual
            this.viewVisual = this.AddViewVisual();

            // View setup
            this.View.ViewLayer = this.viewLayer;
            this.View.ViewLogic = this.viewLogic;
            this.View.ViewVisual = this.viewVisual;
        }

        protected virtual BlockViewVerticalSelectionController AddViewSelection()
        {
            return new IndexBlockViewVerticalSelectionController(this.View);
        }

        protected virtual IViewLogic AddViewLogic()
        {
            return new IndexBlockViewVerticalLogic(
                this.View,
                this.ViewScroll,
                this.ViewSelection,
                this.ViewSwap,
                this.ViewLayout,
                this.ItemFactory);
        }

        protected virtual GradientIndexViewVisual AddViewVisual()
        {
            return new GradientIndexViewVisual(this.View);
        }

        protected virtual void AddViewRegion()
        {
            var graphicsSettings = this.Graphics.Settings;

            var viewRegionSettings = new ViewRegionSettings();
            this.ViewRegion = new ViewRegion(
                regionBounds: () => new Rectangle(
                    location: this.viewSettings.ViewPosition.ToPoint(), 
                    size: new Point(
                        x: graphicsSettings.Width - (int)TestModuleSettings.ViewMargin.X * 2,
                        y: (int)(this.viewSettings.ViewRowDisplay * this.viewSettings.ItemMargin.Y))),
                regionSettings: viewRegionSettings);

            this.View.ViewComponents.Add("ViewRegion", this.ViewRegion);
        }

        protected void AddViewScrollbar()
        {
            var viewVerticalScrollbarSettings = this.viewSettings.Get<ViewScrollbarSettings>("ViewVerticalScrollbar");

            this.viewScrollbar = new ViewVerticalScrollbar(this.viewSettings, this.ViewScroll, this.ViewLayout, this.ViewRegion, viewVerticalScrollbarSettings);

            this.View.ViewComponents.Add("ViewVerticalScrollbar", this.viewScrollbar);

            this.viewLayer.ViewLogic.ScrolledUp   += (sender, args) => this.viewScrollbar.Toggle();
            this.viewLayer.ViewLogic.ScrolledDown += (sender, args) => this.viewScrollbar.Toggle();
            this.viewLayer.ViewLogic.MovedUp      += (sender, args) => this.viewScrollbar.Toggle();
            this.viewLayer.ViewLogic.MovedDown    += (sender, args) => this.viewScrollbar.Toggle();
        }

        protected virtual void AddItemFactory()
        {
            this.ItemFactory = new ViewItemFactory(

                item => new TestItemLayer(item),

                item =>
                {
                    var itemFrame = new TestItemFrameController(item, new ViewItemRectangle(item));

                    var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(
                        item,
                        this.ViewSelection,
                        this.ViewScroll);

                    var itemLayout = new TestItemLayout(
                        item,
                        itemLayoutInteraction);

                    var itemInteraction = new IndexBlockViewVerticalItemInteraction(
                        item,
                        itemLayout,
                        itemLayoutInteraction,
                        new TestIndexedViewBuilder(this.View, this.TestSession));

                    var itemModel = new ViewItemDataModel(item);

                    return new TestItemLogic(
                        item,
                        itemFrame,
                        itemInteraction,
                        itemModel,
                        itemLayout);
                },

                item => new TestItemVisual(item));
        }

        protected virtual void SetupLogic()
        {
            this.View[ViewState.View_Has_Focus] =
                () => this.View[ViewState.View_Has_Selection]() ||
                      this.ViewRegion[RegionState.Region_Has_Focus]() ||
                      this.viewScrollbar[ElementState.Element_Is_Dragging]();
        }
    }
}
