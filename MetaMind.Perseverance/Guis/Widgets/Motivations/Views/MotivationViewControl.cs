using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewControl : ViewControl1D
    {
        #region Constructors

        public MotivationViewControl(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            Region = new ViewRegion(view, viewSettings, itemSettings, RegionPositioning);

            ItemFactory = new MotivationItemFactory();
        }

        #endregion Constructors

        #region Public Properties

        public MotivationItemFactory ItemFactory { get; protected set; }

        public ViewRegion            Region { get; private set; }

        #endregion


        #region Operations

        public void AddItem(MotivationEntry entry)
        {
            View.Items.Add(new ViewItemExchangable(View, ViewSettings, ItemSettings, ItemFactory, entry));
        }

        public void AddItem()
        {
            View.Items.Add(new ViewItemExchangable(View, ViewSettings, ItemSettings, ItemFactory));
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse
            //-----------------------------------------------------------------
            // region
            Region.UpdateInput(gameTime);

            if (AcceptInput)
            {
                // mouse
                //------------------------------------------------------------------
                if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                {
                    Scroll.MoveLeft();
                }

                if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                {
                    Scroll.MoveRight();
                }

                // keyboard
                //------------------------------------------------------------------
                // screen movement
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                {
                    MoveLeft();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    MoveRight();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SLeft))
                {
                    for (var i = 0; i < ViewSettings.ColumnNumDisplay; i++)
                    {
                        MoveLeft();
                    }
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SRight))
                {
                    for (var i = 0; i < ViewSettings.ColumnNumDisplay; i++)
                    {
                        MoveRight();
                    }
                }

                // escape
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                {
                    Selection.Clear();
                }

                // list management
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationCreateItem))
                {
                    AddItem();
                }
            }

            // item input
            //-----------------------------------------------------------------
            foreach (var item in View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

        public override void UpdateStrucutre(GameTime gameTime)
        {
            base  .UpdateStrucutre(gameTime);
            Region.UpdateStructure(gameTime);
        }

        #endregion Update

        #region Configurations

        private Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            var bannerSetting = new ViewBannerSetting();
            return RectangleExt.Rectangle(
                viewSettings.StartPoint.X + viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                viewSettings.StartPoint.Y,
                                            viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                bannerSetting.Height);
        }

        #endregion
    }
}