namespace MetaMind.Testimony.Guis.Modules
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.Xna.Framework;

    using Concepts.Synchronizations;
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
    using Scripting;
    using Sessions;
    using Widgets;

    public class TestModule : Module<TestSettings>
    {
        private readonly GameControllableEntityCollection<IView> entities = new GameControllableEntityCollection<IView>();

        public TestModule(TestSettings settings)
            : base(settings)
        {
        }

        #region Load and Unload
                                                                                                           
        public override void LoadContent(IGameInteropService interop)
        {
            // Data
            var test = Testimony.SessionData.Test;

            var searcher = new ScriptSearcher();
            var loader = new ScriptRunner(searcher);
            loader.Run();

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
            var viewSwap      = new ViewSwapController(view);
            var viewLayout    = new BlockViewVerticalLayout(view);
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
                        itemLayout);
                },

                item => new TestItemVisual(item));

            // View setup
            var viewLogic = new TestViewLogic(
                view,
                viewScroll,
                viewSelection,
                viewSwap,
                viewLayout,
                itemFactory);
            viewLogic.ViewBinding = new TestViewBinding(viewLogic, test.Children);

            var viewVisual = new GradientViewVisual(view);

            this.SetupView(view, viewLayer, viewLogic, viewVisual);

            var viewRegion = this.SetupViewRegion(view, viewSettings);
            var viewScrollbar = this.SetupViewScrollbar(
                view,
                viewSettings,
                viewLayer,
                viewScroll,
                viewLayout,
                viewRegion);

            // Entities
            this.entities.Add(view);

            // Synchronization
            this.SynchronizationData = new SynchronizationData();

            base.LoadContent(interop);
        }

        private void S()
        {
            // Data scanning
            Testimony.FsiSession.EvalScript("Script.fsx");
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.entities.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;
            if (keyboard.IsActionTriggered(KeyboardActions.Enter))
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

        #endregion

        #region Composition

        private void SetupView(
            IView view,
            BlockViewVerticalLayer viewLayer,
            TestViewLogic viewLogic,
            GradientViewVisual viewVisual)
        {
            view.ViewLayer = viewLayer;
            view.ViewLogic = viewLogic;
            view.ViewVisual = viewVisual;

            view.SetupLayer();
            view.SetupBinding();
        }

        private IViewVerticalScrollbar SetupViewScrollbar(
            IView view,
            TestViewSettings viewSettings,
            BlockViewVerticalLayer viewLayer,
            BlockViewVerticalScrollController viewScroll,
            BlockViewVerticalLayout viewLayout,
            ViewRegion viewRegion)
        {
            var viewVerticalScrollbarSettings = viewSettings.Get<ViewScrollbarSettings>("ViewVerticalScrollbar");

            var viewScrollbar = new ViewVerticalScrollbar(
                viewSettings,
                viewScroll,
                viewLayout,
                viewRegion,
                viewVerticalScrollbarSettings);

            view.ViewComponents.Add("ViewVerticalScrollbar", viewScrollbar);

            viewLayer.ViewLogic.ScrolledUp   += (sender, args) => viewScrollbar.Trigger();
            viewLayer.ViewLogic.ScrolledDown += (sender, args) => viewScrollbar.Trigger();
            viewLayer.ViewLogic.MovedUp      += (sender, args) => viewScrollbar.Trigger();
            viewLayer.ViewLogic.MovedDown    += (sender, args) => viewScrollbar.Trigger();

            return viewScrollbar;
        }

        private ViewRegion SetupViewRegion(IView view, TestViewSettings viewSettings)
        {
            var viewRegionSettings = new ViewRegionSettings();

            var viewRegion = new ViewRegion(
                regionBounds: () => new Rectangle(
                    location: viewSettings.ViewPosition.ToPoint(),
                    size: new Point(
                        x: 512 + 128 + 24,
                        y: (int)((viewSettings.ViewRowDisplay - 3) * viewSettings.ItemMargin.Y))),
                regionSettings: viewRegionSettings);

            view.ViewComponents.Add("ViewRegion", viewRegion);

            view[ViewState.View_Has_Focus] = () => 
                viewRegion[RegionState.Region_Has_Focus]() || 
                view[ViewState.View_Has_Selection]();

            return viewRegion;
        }

        #endregion

        #region Synchronization

        public ISynchronizationData SynchronizationData { get; set; }

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

        #endregion
    }
}
