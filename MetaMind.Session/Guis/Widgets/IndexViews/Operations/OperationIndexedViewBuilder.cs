namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using System;
    using Engine.Entities.Controls.Item.Data;
    using Engine.Entities.Controls.Item.Factories;
    using Engine.Entities.Controls.Item.Frames;
    using Engine.Entities.Controls.Item.Interactions;
    using Engine.Entities.Controls.Views;
    using Engine.Entities.Controls.Views.Controllers;
    using Engine.Entities.Controls.Views.Regions;
    using Engine.Entities.Controls.Views.Renderers;
    using Engine.Entities.Controls.Views.Selections;
    using Session.Operations;
    using Tests;

    /// <summary>
    /// Composers are not intended to be reused.
    /// </summary>
    public class OperationIndexedViewBuilder : OperationIndexViewBuilder
    {
        private readonly IMMView viewHost;

        public OperationIndexedViewBuilder(IMMView viewHost, OperationSession operationSeesion)
            : base(operationSeesion)
        {
            if (viewHost == null)
            {
                throw new ArgumentNullException(nameof(viewHost));
            }

            this.viewHost = viewHost;
        }

        public override void Compose(IMMView view, dynamic viewData)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (viewData == null)
            {
                throw new ArgumentNullException(nameof(viewData));
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

                item => new OperationItemLayer(item),

                item =>
                {
                    var itemFrame = new OperationItemFrameController(item, new ViewItemImmRectangle(item));

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
                        new OperationIndexedViewBuilder(this.View, this.OperationSession));

                    var itemModel = new MMViewItemDataModel(item);

                    return new OperationItemLogic(
                        item,
                        itemFrame,
                        itemInteraction,
                        itemModel,
                        itemLayout);
                },

                item => new OperationItemRenderer(item));
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