namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Components.Inputs;
    using Items;
    using Items.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Services;
    using Swaps;

    public class PointViewLogic<TData> : ViewLogic<TData>
    {
        protected PointViewLogic(
            IView view,
            IList<TData> viewData,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        #region Update

        public override void Update(GameTime time)
        {
            if (this.View[ViewState.View_Is_Active]())
            {
                foreach (var item in this.View.ItemsRead.ToArray())
                {
                    item.UpdateView(time);
                    item.Update(time);
                }
            }
            else
            {
                foreach (var item in this.View.ItemsRead.ToArray())
                {
                    item.UpdateView(time);
                }
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommonCreateItem))
            {
                this.AddItem();
            }

            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected void UpdateInputOfItems(IGameInputService input, GameTime time)
        {
            foreach (var item in this.View.ItemsRead.ToArray())
            {
                item.UpdateInput(input, time);
            }
        }

        protected virtual void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.KeyboardEnabled)
                {
                    // Escape
                    if (
                        input.State.Keyboard.IsActionTriggered(
                            KeyboardActions.Escape))
                    {
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        protected virtual void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
        }

        #endregion

        #region Operations

        public void AddItem()
        {
            var item = new ViewItem(this.View, this.View.ItemSettings);

            item.ItemLayer = this.ItemFactory.CreateLayer(item);
            item.ItemData = this.ItemFactory.CreateData(item);
            item.ItemLogic = this.ItemFactory.CreateLogic(item);
            item.ItemVisual = this.ItemFactory.CreateVisual(item);

            item.SetupLayer();

            this.View.ItemsWrite.Add(item);
        }

        #endregion
    }
}