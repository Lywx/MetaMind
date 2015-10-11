namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine.Entities;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Data;
    using Engine.Entities.Controls.Item.Factories;
    using Engine.Entities.Controls.Item.Frames;
    using Engine.Entities.Controls.Item.Interactions;
    using Engine.Entities.Controls.Item.Layers;
    using Engine.Entities.Controls.Item.Settings;
    using Engine.Entities.Controls.Regions;
    using Engine.Entities.Controls.Views;
    using Engine.Entities.Controls.Views.Layouts;
    using Engine.Entities.Controls.Views.Logic;
    using Engine.Entities.Controls.Views.Regions;
    using Engine.Entities.Controls.Views.Scrolls;
    using Engine.Entities.Controls.Views.Selections;
    using Engine.Entities.Controls.Views.Swaps;
    using Engine.Entities.Controls.Views.Visuals;
    using Engine.Entities.Elements;
    using Microsoft.Xna.Framework;
    using Modules;
    using Tests;
    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    /// <summary>
    /// Composers are not intended to be reused.
    /// </summary>
    public class OperationIndexViewBuilder : MMEntity, IIndexViewBuilder
    {
        private StandardIndexViewSettings viewSettings;

        private IMMViewController viewController;

        private IMMViewRenderer viewVisual;

        private MMBlockViewVerticalLayer viewLayer;

        private IViewVerticalScrollbar viewScrollbar;

        public OperationIndexViewBuilder(OperationSession operationSeesion)
        {
            if (operationSeesion == null)
            {
                throw new ArgumentNullException(nameof(operationSeesion));
            }

            this.OperationSession = operationSeesion;
        }

        protected IMMView View { get; set; }

        protected MMBlockViewVerticalScrollController ViewScroll { get; set; }

        protected MMBlockViewVerticalSelectionController ViewSelection { get; set; }

        protected MMIndexBlockViewVerticalLayout ViewLayout { get; set; }

        protected MMViewSwapController ViewSwap { get; set; }

        protected ViewRegion ViewRegion { get; set; }

        protected ViewItemFactory ItemFactory { get; set; }

        protected dynamic ViewData { get; set; }

        protected OperationSession OperationSession { get; set; }

        public virtual void Compose(IMMView view, dynamic viewData)
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

        public IMMView Clone(IMMViewItem item)
        {
            var itemLayer = item.GetLayer<MMBlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLayout;

            var hostViewLayer = item.View.GetLayer<MMBlockViewVerticalLayer>();
            var hostViewSettings = hostViewLayer.ViewSettings;
            var hostViewScroll = hostViewLayer.ViewScroll;

            var indexViewSettings = (StandardIndexViewSettings)item.View.ViewSettings.Clone();
            var indexItemSettings = (ItemSettings)item.View.ItemSettings.Clone();

            indexViewSettings.ViewRowDisplay = hostViewSettings.ViewRowDisplay - itemLayout.Row - itemLayout.BlockRow;
            indexViewSettings.ViewPosition   = hostViewScroll.Position(itemLayout.Row + itemLayout.BlockRow);

            return new MMView(indexViewSettings, indexItemSettings, new List<IMMViewItem>());
        }

        protected void AddView()
        {
            IOperationDescription operation = this.ViewData;

            this.viewSettings = (StandardIndexViewSettings)this.View.ViewSettings;

            // View composition
            this.ViewSelection = this.AddViewSelection();
            this.ViewScroll    = new MMIndexBlockViewVerticalScrollController(this.View);
            this.ViewSwap      = new MMViewSwapController(this.View);
            this.ViewLayout    = new MMIndexBlockViewVerticalLayout(this.View);
            this.viewLayer     = new MMBlockViewVerticalLayer(this.View);

            // Item composition
            this.AddItemFactory();

            // View logic
            this.viewController = this.AddViewLogic();
            this.viewController.ViewBinding = new OperationViewBinding(
                this.viewController,
                operation,
                this.OperationSession);

            // View visual
            this.viewVisual = this.AddViewVisual();

            // View setup
            this.View.ViewLayer = this.viewLayer;
            this.View.ViewController = this.viewController;
            this.View.Renderer = this.viewVisual;
        }

        protected virtual MMBlockViewVerticalSelectionController AddViewSelection()
        {
            return new MMIndexBlockViewVerticalSelectionController(this.View);
        }

        protected virtual IMMViewController AddViewLogic()
        {
            return new MMIndexBlockViewVerticalController(
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
                        x: graphicsSettings.Width - (int)OperationModuleSettings.ViewMargin.X * 2,
                        y: (int)(this.viewSettings.ViewRowDisplay * this.viewSettings.ItemMargin.Y))),
                regionSettings: viewRegionSettings);

            this.View.ViewComponents.Add("ViewRegion", this.ViewRegion);
        }

        protected void AddViewScrollbar()
        {
            var viewVerticalScrollbarSettings = this.viewSettings.Get<ViewScrollbarSettings>("ViewVerticalScrollbar");

            this.viewScrollbar = new ViewVerticalScrollbar(this.viewSettings, this.ViewScroll, this.ViewLayout, this.ViewRegion, viewVerticalScrollbarSettings);

            this.View.ViewComponents.Add("ViewVerticalScrollbar", this.viewScrollbar);

            this.viewLayer.ViewController.ScrolledUp   += (sender, args) => this.viewScrollbar.Toggle();
            this.viewLayer.ViewController.ScrolledDown += (sender, args) => this.viewScrollbar.Toggle();
            this.viewLayer.ViewController.MovedUp      += (sender, args) => this.viewScrollbar.Toggle();
            this.viewLayer.ViewController.MovedDown    += (sender, args) => this.viewScrollbar.Toggle();
        }

        protected virtual void AddItemFactory()
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
                        itemLayoutInteraction);

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

        protected virtual void SetupLogic()
        {
            this.View[MMViewState.View_Has_Focus] =
                () => this.View[MMViewState.View_Has_Selection]() ||
                      this.ViewRegion[RegionState.Region_Has_Focus]() ||
                      this.viewScrollbar[MMElementState.Element_Is_Dragging]();
        }
    }
}
