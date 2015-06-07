namespace MetaMind.Testimony.Guis.Modules
{
    using System.Collections.Generic;
    using System.Linq;
    using Concepts.Synchronizations;
    using Concepts.Tests;
    using Engine;
    using Engine.Components.Events;
    using Engine.Components.Inputs;
    using Engine.Guis;
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
    using Events;
    using Sessions;
    using Widgets;

    using Microsoft.Xna.Framework;

    public class TestModule : Module<TestSettings>
    {
        private readonly GameControllableEntityCollection<View> entities;

        public TestModule(TestSettings settings)
            : base(settings)
        {
            this.entities = new GameControllableEntityCollection<View>();

            var tests = new List<Test>();

            // View settings
            var viewSettings = new TestViewSettings(
                itemMargin    : new Vector2(512 + 128 + 24, 26),
                viewPosition  : new Vector2(40, 100),
                viewRowDisplay: 30, 
                viewRowMax    : 100);

            // Item settings
            var itemSettings = new TestItemSettings();

            // View
            var view = new View(viewSettings, itemSettings, new List<IViewItem>());

            // View composition
            var viewSelection = new BlockViewVerticalSelectionController(view);
            var viewScroll    = new BlockViewVerticalScrollController(view);
            var viewSwap      = new ViewSwapController<Test>(view, tests);
            var viewLayout    = new BlockViewVerticalLayout(view);

            var viewLayer = new BlockViewVerticalLayer(view);
            var itemFactory = new ViewItemFactory(
                item => new TestItemLayer(item),
                item =>
                {
                    var itemFrame             = new TestItemFrame(item);
                    var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(item, viewSelection, viewScroll);
                    var itemLayout            = new TestItemLayout(item, itemLayoutInteraction);
                    var itemInteraction       = new BlockViewVerticalItemInteraction(item, itemLayout, itemLayoutInteraction);
                    var itemModel             = new ViewItemDataModel(item);

                    return new TestItemLogic(item, itemFrame, itemInteraction, itemModel, itemLayout);
                },
                item => new TestItemVisual(item),
                item =>
                {
                    var test = new Test();
                    tests.Add(test);
                    return test;
                });
            var viewLogic = new TestViewLogic<Test>(view, tests, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory);
            var viewVisual = new GradientViewVisual(view);

            view.ViewLayer  = viewLayer;
            view.ViewLogic  = viewLogic;
            view.ViewVisual = viewVisual;

            view.SetupLayer();

            // View region 
            var viewRegionSettings = new ViewRegionSettings();
            var viewRegion = new ViewRegion(
                regionBounds: () => new Rectangle(
                    location: viewSettings.ViewPosition.ToPoint(),
                    size: new Point(
                        x: 512 + 128 + 24,
                        y: (int)((viewSettings.ViewRowDisplay - 3) * viewSettings.ItemMargin.Y))),
                regionSettings: viewRegionSettings);
            view.ViewComponents.Add("ViewRegion", viewRegion);
            view[ViewState.View_Has_Focus] = () => viewRegion[RegionState.Region_Has_Focus]() || view[ViewState.View_Has_Selection]();

            // View scrollbar
            var viewVerticalScrollbarSettings = viewSettings.Get<ViewScrollbarSettings>("ViewVerticalScrollbar");
            var viewVerticalScrollbar = new ViewVerticalScrollbar(viewSettings, viewScroll, viewLayout, viewRegion, viewVerticalScrollbarSettings);
            view.ViewComponents.Add("ViewVerticalScrollbar", viewVerticalScrollbar);
            viewLayer.ViewLogic.ScrolledUp   += (sender, args) => viewVerticalScrollbar.Trigger();
            viewLayer.ViewLogic.ScrolledDown += (sender, args) => viewVerticalScrollbar.Trigger();
            viewLayer.ViewLogic.MovedUp      += (sender, args) => viewVerticalScrollbar.Trigger();
            viewLayer.ViewLogic.MovedDown    += (sender, args) => viewVerticalScrollbar.Trigger();

            // Entities
            this.entities.Add(view);

            this.SynchronizationData = new SynchronizationData();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Enter))
            {
                this.SwitchSync();
            }

            this.entities.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.entities.UpdateForwardBuffer();
            this.entities.Update(time);
            this.entities.UpdateBackwardBuffer();
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.entities.Draw(graphics, time, alpha);
        }

        #region Pure Synchronization

        public void StartSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStarted, new SynchronizationStartedEventArgs(this.SynchronizationData)));
        }

        public void StopSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStopped, new SynchronizationStoppedEventArgs(this.SynchronizationData)));
        }

        public void SwitchSync()
        {
            if (!this.SynchronizationData.IsSynchronizing)
            {
                this.StartSync();
            }
            else
            {
                this.StopSync();
            }
        }

        public ISynchronizationData SynchronizationData { get; set; }

        #endregion
    }
}
