namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using System;
    using Concepts.Tests;
    using Engine.Guis.Controls.Items.Data;
    using Engine.Guis.Controls.Items.Factories;
    using Engine.Guis.Controls.Items.Frames;
    using Engine.Guis.Controls.Items.Interactions;
    using Engine.Guis.Controls.Views;
    using Engine.Guis.Controls.Views.Logic;
    using Engine.Guis.Controls.Views.Regions;
    using Engine.Guis.Controls.Views.Selections;
    using Engine.Guis.Controls.Views.Visuals;

    /// <summary>
    /// Composers are not intended to be reused.
    /// </summary>
    public class TestIndexedViewCompositor : TestIndexViewCompositor
    {
        private readonly IView viewHost;

        public TestIndexedViewCompositor(IView viewHost, TestSession testSeesion)
            : base(testSeesion)
        {
            if (viewHost == null)
            {
                throw new ArgumentNullException("viewHost");
            }

            this.viewHost = viewHost;
        }

        public override void Compose(IView view, dynamic viewData)
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

        protected override IViewLogic AddViewLogic()
        {
            return new IndexedBlockViewVerticalLogic(
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
                    var itemFrame = new TestItemFrame(item, new ViewItemPickableFrame(item));

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
                                this.View[ViewState.View_Is_Active]() &&
                                hostViewRegion.RegionBounds().Contains(itemFrame.NameFrame.Center);
                        }
                    };

                    var itemInteraction = new IndexBlockViewVerticalItemInteraction(
                        item,
                        itemLayout,
                        itemLayoutInteraction, 
                        new TestIndexedViewCompositor(this.View, this.TestSession));

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
            this.View[ViewState.View_Has_Focus] =
                () => this.View[ViewState.View_Has_Selection]();
        }
    }
}