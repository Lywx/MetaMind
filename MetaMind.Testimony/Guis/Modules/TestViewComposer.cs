namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
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
    using Microsoft.Xna.Framework;
    using Widgets;

    public class TestViewComposer : IIndexViewComposer
    {
        private readonly TestSession testSession;

        public TestViewComposer(TestSession testSeesion)
        {
            if (testSeesion == null)
            {
                throw new ArgumentNullException("testSeesion");
            }

            this.testSession = testSeesion;
        }

        public void Compose(IView view, dynamic data)
        {
            var test = (Test)data;

            var viewSettings = (TestViewSettings)view.ViewSettings;

            // View composition
            var viewSelection = new BlockViewVerticalSelectionController(view);
            var viewScroll    = new BlockViewVerticalScrollController(view);
            var viewSwap      = new ViewSwapController(view);
            var viewLayout    = new IndexBlockViewVerticalLayout(view);
            var viewLayer     = new BlockViewVerticalLayer(view);

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
                        itemLayout,
                        new TestViewComposer(this.testSession));
                },

                item => new TestItemVisual(item));

            // View logic
            var viewLogic = new TestViewLogic(
                view,
                viewScroll,
                viewSelection,
                viewSwap,
                viewLayout,
                itemFactory);
            viewLogic.ViewBinding = new TestViewBinding(
                viewLogic,
                test.Children,
                this.testSession);

            // View visual
            var viewVisual = new GradientViewVisual(view);

            // View setup
            view.ViewLayer  = viewLayer;
            view.ViewLogic  = viewLogic;
            view.ViewVisual = viewVisual;

            // View region
            var viewRegion = AddViewRegion(view, viewSettings);

            // View scrollbar
            var viewScrollbar = this.AddViewScrollbar(view, viewSettings, viewLayer, viewScroll, viewLayout, viewRegion);

            // View focus
            view[ViewState.View_Has_Focus] =
                () => view[ViewState.View_Has_Selection]() ||
                      viewRegion[RegionState.Region_Has_Focus]() || 
                      viewScrollbar[FrameState.Frame_Is_Dragging]();
        }

        public IView Construct(IViewItem item)
        {
            var itemLayer = item.GetLayer<BlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLayout;

            var viewLayer = item.View.GetLayer<BlockViewVerticalLayer>();
            var viewSettings = viewLayer.ViewSettings;
            var viewScroll = viewLayer.ViewScroll;

            var hostViewSettings = (TestViewSettings)item.View.ViewSettings.Clone();
            var hostItemSettings = (TestItemSettings)item.View.ItemSettings.Clone();

            hostViewSettings.ViewRowDisplay = viewSettings.ViewRowDisplay - itemLayout.Row - itemLayout.BlockRow;
            hostViewSettings.ViewPosition = viewScroll.Position(itemLayout.Row + itemLayout.BlockRow);

            return new View(hostViewSettings, hostItemSettings, new List<IViewItem>());
        }

        private static ViewRegion AddViewRegion(
            IView view,
            TestViewSettings viewSettings)
        {
            var viewRegionSettings = new ViewRegionSettings();
            var viewRegion = new ViewRegion(
                regionBounds: () => new Rectangle(
                    location: viewSettings.ViewPosition.ToPoint(), 
                    size: new Point(
                        x: 1355 + 128 + 24,
                        y: (int)(viewSettings.ViewRowDisplay * viewSettings.ItemMargin.Y))),
                regionSettings: viewRegionSettings);

            view.ViewComponents.Add("ViewRegion", viewRegion);

            return viewRegion;
        }

        private IViewVerticalScrollbar AddViewScrollbar(
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

    }
}
