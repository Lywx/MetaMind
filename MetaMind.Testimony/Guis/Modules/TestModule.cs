namespace MetaMind.Testimony.Guis.Modules
{
    using System.Collections.Generic;

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
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Views;
    using Engine.Guis.Widgets.Views.Layers;
    using Engine.Guis.Widgets.Views.Layouts;
    using Engine.Guis.Widgets.Views.Scrolls;
    using Engine.Guis.Widgets.Views.Selections;
    using Engine.Guis.Widgets.Views.Settings;
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

            var test = new List<Test>();

            var view = new View(
                    new PointViewVerticalSettings(
                        position     : new Vector2(40, 100),
                        margin       : new Vector2(512 + 64 + 24, 26),
                        rowNumDisplay: 30,
                        rowNumMax    : 100),

                    new TestItemSettings(),
                    new List<IViewItem>());

            var viewSelection = new PointViewVerticalSelectionController(view);
            var viewScroll = new BlockViewVerticalScrollController(view);
            var viewSwap = new BlockViewVerticalSwapController<Test>(view, test);
            var viewLayout = new BlockViewVerticalLayout(view);

            var itemFactory = new ViewItemFactory(
                item => new TestItemLayer(item),
                item =>
                {
                    var itemFrame             = new TestItemFrame(item);
                    var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(item, viewSelection, viewScroll);
                    var itemLayout            = new BlockViewVerticalItemLayout(item, itemLayoutInteraction);
                    var itemInteraction       = new PointViewItemInteraction(item, itemLayout, itemLayoutInteraction);
                    var itemModel             = new ViewItemDataModel(item);

                    return new TestItemLogic(item, itemFrame, itemInteraction, itemModel, itemLayout);
                },
                item => new TestItemVisual(item),
                item =>
                {
                    var newTest = new Test("A Sample Knowing that millions of people around the world would be watching in person and on television and expecting great things from him — at least one more gold medal for America, if not another world record — during this, his fourth and surely his last appearance in the World Olympics, and realizing that his legs could no longer carry him down t");
                    test.Add(newTest);
                    return newTest;
                });

            view.ViewLayer  = new BlockViewVerticalLayer(view);
            view.ViewLogic  = new TestViewLogic<Test>(view, test, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory);
            view.ViewVisual = new ViewVisual(view);

            view.SetupLayer();

            view[ViewState.View_Has_Focus] = () => true;

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
