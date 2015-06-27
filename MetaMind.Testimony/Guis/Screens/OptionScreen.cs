namespace MetaMind.Testimony.Guis.Screens
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine;
    using Engine.Guis.Layers;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Factories;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layers;
    using Engine.Guis.Widgets.Views;
    using Engine.Guis.Widgets.Views.Layouts;
    using Engine.Guis.Widgets.Views.Logic;
    using Engine.Guis.Widgets.Views.Scrolls;
    using Engine.Guis.Widgets.Views.Selections;
    using Engine.Guis.Widgets.Views.Swaps;
    using Engine.Guis.Widgets.Views.Visuals;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Widgets.BlockViews.Options;
    using Widgets.IndexViews.Tests;

    public class OptionScreen : GameScreen
    {
        private readonly IGameLayer hiddenLayer;

        private readonly List<IOption> options;

        private View view;

        public OptionScreen(IGameLayer hiddenLayer, List<IOption> options)
        {
            if (hiddenLayer == null)
            {
                throw new ArgumentNullException("hiddenLayer");
            }

            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            this.hiddenLayer = hiddenLayer;
            this.options     = options;

            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;

            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Entities = new GameControllableEntityCollection<IGameControllableEntity>();
        }

        protected GameControllableEntityCollection<IGameControllableEntity> Entities { get; private set; }

        public override void LoadContent(IGameInteropService interop)
        {
            // View settings
            var viewSettings = new OptionViewSettings(
                itemMargin    : new Vector2(1355 + 128 + 24, 26),
                viewPosition  : new Vector2(40, 100),
                viewRowDisplay: 30,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new OptionItemSettings();

            // View composition
            this.view = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewScroll    = new BlockViewVerticalScrollController(this.view);
            var viewSelection = new BlockViewVerticalSelectionController(this.view);
            var viewLayout    = new BlockViewVerticalLayout(this.view);
            var viewSwap      = new ViewSwapController(this.view);

            var itemFactory =
                new ViewItemFactory(

                    item => new OptionItemLayer(item),

                    item =>
                    {
                        var itemFrame             = new OptionItemFrame(item, new ViewItemPickableFrame(item));
                        var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(item, viewSelection, viewScroll);
                        var itemLayout            = new TestItemLayout(item, itemLayoutInteraction);
                        var itemInteraction       = new BlockViewVerticalItemInteraction(item, itemLayout, itemLayoutInteraction);
                        var itemModel             = new ViewItemDataModel(item);

                        return new OptionItemLogic(item, itemFrame, itemInteraction, itemModel, itemLayout);
                    },

                    item => new OptionItemVisual(item));

            var viewLogic = new BlockViewVerticalLogic(this.view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
            {
                ViewBinding = new OptionViewBinding(this.options)
            };
            var viewVisual = new GradientViewVisual(this.view);
            var viewLayer  = new BlockViewVerticalLayer(this.view);
            this.view.ViewLayer = viewLayer;
            this.view.ViewLogic = viewLogic;
            this.view.ViewVisual = viewVisual;

            this.Entities.Add(this.view);

            this.Entities.LoadContent(interop);
            base.LoadContent(interop);

            this.hiddenLayer.FadeOut(TimeSpan.FromSeconds(0.5));

            // Load Items
            this.view.ViewLogic.ResetItems();
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            base.UnloadContent(interop);

            this.hiddenLayer.FadeIn(TimeSpan.FromSeconds(0.5));
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            graphics.SpriteBatch.Begin();

            this.Entities.Draw(graphics, time, this.TransitionAlpha);

            graphics.SpriteBatch.End();

            base.Draw(graphics, time);
        }

        public override void Update(GameTime time)
        {
            this.Entities.UpdateForwardBuffer();
            this.Entities.Update(time);
            this.Entities.UpdateBackwardBuffer();

            base.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);
            base.UpdateInput(input, time);
        }
    }
}
