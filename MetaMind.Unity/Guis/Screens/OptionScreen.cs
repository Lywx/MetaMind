namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine;
    using Engine.Components.Fonts;
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
    using Engine.Guis.Widgets.Visuals;
    using Engine.Screens;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;
    using Widgets.BlockViews.Options;
    using Widgets.IndexViews.Tests;

    public class OptionScreen : GameScreen
    {
        private readonly string procedureName;

        private readonly string procedureDescription;

        private readonly IGameLayer backgroundLayer;

        private readonly List<IOption> options;

        private IView optionView;

        private LabelBox procedureDescriptionLabelBox;

        private LabelBox procedureNameLabelBox;

        private Label screenLabel;

        public OptionScreen(string procedureName, string procedureDescription, List<IOption> options, IGameLayer backgroundLayer)
        {
            if (procedureName == null)
            {
                throw new ArgumentNullException("procedureName");
            }

            if (procedureDescription == null)
            {
                throw new ArgumentNullException("procedureDescription");
            }

            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (backgroundLayer == null)
            {
                throw new ArgumentNullException("backgroundLayer");
            }

            this.procedureName        = procedureName;
            this.procedureDescription = procedureDescription;

            this.backgroundLayer = backgroundLayer;
            this.options = options;

            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;

            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Entities = new GameControllableEntityCollection<IGameControllableEntity>();
        }

        private GameControllableEntityCollection<IGameControllableEntity> Entities { get; set; }

        public override void LoadContent(IGameInteropService interop)
        {
            const int viewWidth = 1355 + 128 + 24;
            
            // Screen label
            this.screenLabel = new Label
            {
                TextFont     = () => Font.UiRegular,
                Text         = () => "OPTIONS",
                TextPosition = () => new Vector2(this.GameGraphics.Settings.Width / 2, 70),
                TextColor    = () => Palette.Transparent5,
                TextSize     = () => 1f,
                TextHAlign   = StringHAlign.Center,
            };

            this.procedureNameLabelBox = new LabelBox(
                new LabelSettings
                {
                    TextFont       = Font.ContentRegular,
                    Text           = () => this.procedureName,
                    TextPosition   = () => new Vector2(40, 100),
                    TextColor      = Color.White,
                    TextSize       = 0.8f,
                    TextLeading    = 26,
                    TextMonospaced = true
                },
                new Vector2(5, 12) * 0.8f, 
                new BoxSettings(() => new Rectangle(40, 100, viewWidth, 0))
                {
                    Color = () => Palette.DimBlue,
                    ColorFilled = () => true,
                });

            this.procedureDescriptionLabelBox = new LabelBox(
                new LabelSettings
                {
                    TextFont       = Font.ContentRegular,
                    Text           = () => this.procedureDescription,
                    TextPosition   = () => new Vector2(40, this.procedureNameLabelBox.Bottom),
                    TextColor      = Color.White,
                    TextSize       = 0.8f,
                    TextLeading    = 26,
                    TextMonospaced = true
                },
                new Vector2(5, 12) * 0.8f,
                new BoxSettings(() => new Rectangle(40, this.procedureNameLabelBox.Bottom, viewWidth, 0))
                {
                    Color       = () => Palette.Transparent1,
                    ColorFilled = () => true,
                });

            var graphics = interop.Engine.Graphics;

            // View settings
            var viewSettings = new OptionViewSettings(
                itemMargin    : new Vector2(viewWidth, 26),
                viewPosition  : new Vector2(40, this.procedureDescriptionLabelBox.Bottom),
                viewRowDisplay: (graphics.Settings.Height - this.procedureDescriptionLabelBox.Bottom - 72) / 26,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new OptionItemSettings();

            // View composition
            this.optionView = new View(viewSettings, itemSettings, new List<IViewItem>());

            var viewScroll    = new BlockViewVerticalScrollController(this.optionView);
            var viewSelection = new BlockViewVerticalSelectionController(this.optionView);
            var viewLayout    = new BlockViewVerticalLayout(this.optionView);
            var viewSwap      = new ViewSwapController(this.optionView);

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

            var viewLogic = new BlockViewVerticalLogic(this.optionView, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
            {
                ViewBinding = new OptionViewBinding(this.options)
            };
            var viewVisual = new GradientViewVisual(this.optionView);
            var viewLayer  = new BlockViewVerticalLayer(this.optionView);
            this.optionView.ViewLayer  = viewLayer;
            this.optionView.ViewLogic  = viewLogic;
            this.optionView.ViewVisual = viewVisual;

            this.Entities.Add(this.optionView);

            this.Entities.LoadContent(interop);
            base.LoadContent(interop);

            this.backgroundLayer.FadeOut(TimeSpan.FromSeconds(0.5));
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            base.UnloadContent(interop);

            this.backgroundLayer.FadeIn(TimeSpan.FromSeconds(0.5));
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            graphics.SpriteBatch.Begin();

            this.procedureNameLabelBox       .Draw(graphics, time, this.TransitionAlpha);
            this.procedureDescriptionLabelBox.Draw(graphics, time, this.TransitionAlpha);

            this.screenLabel.Draw(graphics, time, this.TransitionAlpha);
            this.Entities   .Draw(graphics, time, this.TransitionAlpha);

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
