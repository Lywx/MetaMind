namespace MetaMind.Session.Guis.Screens
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine.Components.Content.Fonts;
    using Engine.Entities;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Item.Factories;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Item.Layers;
    using Engine.Gui.Controls.Labels;
    using Engine.Gui.Controls.Views;
    using Engine.Gui.Controls.Views.Layouts;
    using Engine.Gui.Controls.Views.Logic;
    using Engine.Gui.Controls.Views.Scrolls;
    using Engine.Gui.Controls.Views.Selections;
    using Engine.Gui.Controls.Views.Swaps;
    using Engine.Gui.Controls.Views.Visuals;
    using Engine.Gui.Graphics.Fonts;
    using Engine.Screen;
    using Engine.Services;
    using Engine.Settings.Color;
    using Microsoft.Xna.Framework;
    using Modules;
    using Widgets.BlockViews.Options;
    using Widgets.IndexViews.Tests;

    public class OptionScreen : MMScreen
    {
        private readonly List<IOption> options;

        private IMMViewNode optionView;

        private readonly string procedureName;

        private readonly string procedureDescription;

        private LabelBox procedureDescriptionLabelBox;

        private LabelBox procedureNameLabelBox;

        private readonly IMMLayer screenBackground;

        private Label screenLabel;

        public OptionScreen(string procedureName, string procedureDescription, List<IOption> options, IMMLayer screenBackground)
        {
            if (procedureName == null)
            {
                throw new ArgumentNullException(nameof(procedureName));
            }

            if (procedureDescription == null)
            {
                throw new ArgumentNullException(nameof(procedureDescription));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (screenBackground == null)
            {
                throw new ArgumentNullException(nameof(screenBackground));
            }

            this.procedureName        = procedureName;
            this.procedureDescription = procedureDescription;

            this.options = options;

            this.screenBackground = screenBackground;

            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;

            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Entities = new MMEntityCollection<IMMInputEntity>();
        }

        private MMEntityCollection<IMMInputEntity> Entities { get; set; }

        public override void LoadContent(IMMEngineInteropService interop)
        {
            var graphicsSettings = this.Graphics.Settings;

            var viewWidth = graphicsSettings.Width - OperationModuleSettings.ViewMargin.X * 2;
            
            // Screen label
            this.screenLabel = new Label
            {
                TextFont     = () => Font.UiRegular,
                Text         = () => "OPTIONS",
                AnchorLocation = () => new Vector2(graphicsSettings.Width / 2.0f, 80),
                TextColor    = () => this.Color.White,
                TextSize     = () => 1f,
                TextHAlignment   = HoritonalAlignment.Center,
                TextVAlignment   = VerticalAlignment.Center,
            };

            this.procedureNameLabelBox = new LabelBox(
                new LabelSettings
                {
                    Font       = Font.ContentRegular,
                    Text           = () => this.procedureName,
                    AnchorLocation   = () => OperationModuleSettings.ViewMargin.ToVector2(),
                    Color      = this.Color.White,
                    Size       = 0.8f,
                    Leading    = OperationModuleSettings.ItemMargin.Y,
                    Monospaced = true
                },
                new Vector2(5, 12) * 0.8f, 
                new ColorBoxSettings(() => new Rectangle(OperationModuleSettings.ViewMargin, new Point(viewWidth, 0)))
                {
                    Color = () => MMPalette.DimBlue,
                    ColorFilled = () => true,
                });

            this.procedureDescriptionLabelBox = new LabelBox(
                new LabelSettings
                {
                    Font       = Font.ContentRegular,
                    Text           = () => this.procedureDescription,
                    AnchorLocation   = () => new Vector2(OperationModuleSettings.ViewMargin.X, this.procedureNameLabelBox.Bottom),
                    Color      = this.Color.White,
                    Size       = 0.8f,
                    Leading    = OperationModuleSettings.ItemMargin.Y,
                    Monospaced = true
                },
                new Vector2(5, 12) * 0.8f,
                new ColorBoxSettings(() => new Rectangle((int)OperationModuleSettings.ViewMargin.X, this.procedureNameLabelBox.Bottom, viewWidth, 0))
                {
                    Color       = () => MMPalette.Transparent20,
                    ColorFilled = () => true,
                });

            // View settings
            var viewSettings = new OptionViewSettings(
                itemMargin    : new Vector2(viewWidth, OperationModuleSettings.ItemMargin.Y),
                viewPosition  : new Vector2(OperationModuleSettings.ViewMargin.X, this.procedureDescriptionLabelBox.Bottom),
                viewRowDisplay: (graphicsSettings.Height - this.procedureDescriptionLabelBox.Bottom - 72) / (int)OperationModuleSettings.ItemMargin.Y,
                viewRowMax    : int.MaxValue);

            // Item settings
            var itemSettings = new OptionItemSettings();

            // View composition
            this.optionView = new MMViewNode(viewSettings, itemSettings, new List<IViewItem>());

            var viewScroll    = new BlockViewVerticalScrollController(this.optionView);
            var viewSelection = new BlockViewVerticalSelectionController(this.optionView);
            var viewLayout    = new BlockViewVerticalLayout(this.optionView);
            var viewSwap      = new ViewSwapController(this.optionView);

            var itemFactory =
                new ViewItemFactory(

                    item => new OptionItemLayer(item),

                    item =>
                    {
                        var itemFrame             = new OptionItemFrameController(item, new ViewItemImmRectangle(item));
                        var itemLayoutInteraction = new BlockViewVerticalItemLayoutInteraction(item, viewSelection, viewScroll);
                        var itemLayout            = new TestItemLayout(item, itemLayoutInteraction);
                        var itemInteraction       = new BlockViewVerticalItemInteraction(item, itemLayout, itemLayoutInteraction);
                        var itemModel             = new ViewItemDataModel(item);

                        return new OptionItemLogic(item, itemFrame, itemInteraction, itemModel, itemLayout);
                    },

                    item => new OptionItemVisual(item));

            var viewLogic = new MMBlockViewVerticalController(this.optionView, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
            {
                ViewBinding = new OptionViewBinding(this.options)
            };
            var viewVisual = new GradientViewVisual(this.optionView);
            var viewLayer  = new BlockViewVerticalLayer(this.optionView);
            this.optionView.ViewLayer  = viewLayer;
            this.optionView.ViewController  = viewLogic;
            this.optionView.ViewVisual = viewVisual;

            this.Entities.Add(this.optionView);

            this.Entities.LoadContent(interop);
            base.LoadContent(interop);

            this.screenBackground.FadeOut(TimeSpan.FromSeconds(0.5));
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            base.UnloadContent(interop);

            this.screenBackground.FadeIn(TimeSpan.FromSeconds(0.5));
        }

        public override void Draw(GameTime time)
        {
            this.SpriteBatch.Begin();

            this.procedureNameLabelBox.Draw(time);
            this.procedureDescriptionLabelBox.Draw(time);

            this.screenLabel.Draw(time);
            this.Entities.Draw(time);

            this.SpriteBatch.End();

            base.Draw(time);
        }

        public override void Update(GameTime time)
        {
            this.Entities.UpdateForwardBuffer();
            this.Entities.Update(time);
            this.Entities.UpdateBackwardBuffer();
            base.Update(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);
            base.UpdateInput(input, time);
        }
    }
}
