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
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Views;
    using Engine.Guis.Widgets.Views.Layers;
    using Engine.Guis.Widgets.Views.Layouts;
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

            var test = new List<Test>();

            var view = new View(new TestViewSettings(new Vector2(50, 50),
                        new Vector2(50, 26), 1, 1, 30, 100),
                    new TestItemSettings(),
                    new List<IViewItem>());
            view.ViewLayer = new PointView2DLayer(view);

            var viewSelection = new PointView2DSelectionController(view);
            var viewScroll = new PointView2DScrollController(view);
            var viewSwap = new PointViewHorizontalSwapController<Test>(view, test);
            var viewLayout = new PointView2DLayout(view);
            
            view.ViewLogic = new TestViewLogic<Test>(view,
                test,
                viewScroll,
                viewSelection,
                viewSwap,
                viewLayout,
                new ViewItemFactory(
                    item => new TestItemLayer(item),
                    delegate(IViewItem item)
                    {
                        var itemFrame = new TestItemFrame(item);
                        var itemLayoutInteraction = new ViewItemLayoutInteraction(item, viewSelection, viewScroll);
                        var itemLayout = new PointView2DItemLayout(item, itemLayoutInteraction);
                        var itemInteraction = new PointView2DItemInteraction(item, itemLayout, itemLayoutInteraction);
                        var itemModel = new ViewItemDataModel(item);

                        return new TestItemLogic(item, itemFrame, itemInteraction, itemModel, itemLayout);
                    },
                    item => new TestItemVisual(item),
                    item =>
                    {
                        var test1 = new Test( "A Sample Txt aisjd lkasjf laskjd glksjfl ksdjfl kasdjl");
                        test.Add(test1);
                        return test1;
                    })
                );
            view.ViewVisual = new ViewVisual(view);
            view[ViewState.View_Has_Focus] = () => true;
            view.SetupLayer();

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
            this.entities.Update(time);
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
            @event.QueueEvent(new Event((int) SessionEventType.SyncStopped, new SynchronizationStoppedEventArgs(this.SynchronizationData)));
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
