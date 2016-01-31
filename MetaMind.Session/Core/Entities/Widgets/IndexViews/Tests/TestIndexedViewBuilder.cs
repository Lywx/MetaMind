namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using System;
    using Engine.Core.Entity.Control.Item.Data;
    using Engine.Core.Entity.Control.Item.Factories;
    using Engine.Core.Entity.Control.Item.Frames;
    using Engine.Core.Entity.Control.Item.Interactions;
    using Engine.Core.Entity.Control.Views;
    using Engine.Core.Entity.Control.Views.Controllers;
    using Engine.Core.Entity.Control.Views.Regions;
    using Engine.Core.Entity.Control.Views.Renderers;
    using Engine.Core.Entity.Control.Views.Selections;
    using Session.Tests;

    /// <summary>
    /// Composers are not intended to be reused.
    /// </summary>
    public class TestIndexedViewBuilder : TestIndexViewBuilder
    {
        private readonly IMMView viewHost;

        public TestIndexedViewBuilder(IMMView viewHost, TestSession testSeesion)
            : base(testSeesion)
        {
            if (viewHost == null)
            {
                throw new ArgumentNullException("viewHost");
            }

            this.viewHost = viewHost;
        }

        public override void Compose(IMMView view, dynamic viewData)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (viewData == null)
            {
                throw new ArgumentNullException("viewData");
            }

            this.View = view;
            this.ViewData = viewData;

            this.AddView();
            this.AddViewRegion();

            this.InitializeLogic();
        }

        protected override MMBlockViewVerticalSelectionController AddViewSelection()
        {
            return new MMIndexedBlockViewVerticalSelectionController(this.View);
        }

        protected override IMMViewController AddViewLogic()
        {
            return new MMIndexedBlockViewVerticalController(
                this.View,
                this.ViewScroll,
                this.ViewSelection,
                this.ViewSwap,
                this.ViewLayout,
                this.ItemFactory);
        }

        protected override GradientIndexViewVisual AddViewVisual()
        {
            return new GradientIndexedViewVisual(this.View);
        }

        protected override void AddItemFactory()
        {
            this.ItemFactory = new ViewItemFactory(

                item => new TestItemLayer(item),

                item =>
                {
                    var itemFrame = new TestItemFrameController(item, new ViewItemImmRectangle(item));

                    var itemLayoutInteraction = new MMBlockViewVerticalItemLayoutInteraction(
                        item,
                        this.ViewSelection,
                        this.ViewScroll);

                    var itemLayout = new TestItemLayout(
                        item,
                        itemLayoutInteraction)
                    {
                        ItemIsActive = () =>
                        {
                            var hostViewRegion = this.viewHost.GetComponent<ViewRegion>("ViewRegion");

                            return 
                                this.View[MMViewState.View_Is_Active]() &&
                                hostViewRegion.RegionBounds().Contains(itemFrame.NameImmRectangle.Center);
                        }
                    };

                    var itemInteraction = new MMIndexBlockViewVerticalItemInteraction(
                        item,
                        itemLayout,
                        itemLayoutInteraction, 
                        new TestIndexedViewBuilder(this.View, this.TestSession));

                    var itemModel = new MMViewItemDataModel(item);

                    return new TestItemLogic(
                        item,
                        itemFrame,
                        itemInteraction,
                        itemModel,
                        itemLayout);
                },

                item => new TestItemRenderer(item));
        }

        protected override void AddViewRegion()
        {
            var hostViewRegion = this.viewHost.GetComponent<ViewRegion>("ViewRegion");
            this.View.ViewComponents.Add("ViewRegion", hostViewRegion);
        }

        protected override void InitializeLogic()
        {
            this.View[MMViewState.View_Has_Focus] =
                () => this.View[MMViewState.View_Has_Selection]();
        }
    }
}