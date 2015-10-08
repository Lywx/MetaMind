namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using System;
    using Concepts.Tests;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Item.Factories;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Views;
    using Engine.Gui.Controls.Views.Logic;
    using Engine.Gui.Controls.Views.Regions;
    using Engine.Gui.Controls.Views.Selections;
    using Engine.Gui.Controls.Views.Visuals;

    /// <summary>
    /// Composers are not intended to be reused.
    /// </summary>
    public class TestIndexedViewBuilder : TestIndexViewBuilder
    {
        private readonly IMMViewNode viewHost;

        public TestIndexedViewBuilder(IMMViewNode viewHost, TestSession testSeesion)
            : base(testSeesion)
        {
            if (viewHost == null)
            {
                throw new ArgumentNullException("viewHost");
            }

            this.viewHost = viewHost;
        }

        public override void Compose(IMMViewNode view, dynamic viewData)
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

            this.SetupLogic();
        }

        protected override BlockViewVerticalSelectionController AddViewSelection()
        {
            return new IndexedBlockViewVerticalSelectionController(this.View);
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

                    var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(
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

        protected override void AddViewRegion()
        {
            var hostViewRegion = this.viewHost.GetComponent<ViewRegion>("ViewRegion");
            this.View.ViewComponents.Add("ViewRegion", hostViewRegion);
        }

        protected override void SetupLogic()
        {
            this.View[MMViewState.View_Has_Focus] =
                () => this.View[MMViewState.View_Has_Selection]();
        }
    }
}