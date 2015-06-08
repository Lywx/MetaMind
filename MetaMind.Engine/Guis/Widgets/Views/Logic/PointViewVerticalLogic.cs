namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Components.Inputs;
    using Items.Data;
    using Items.Factories;
    using Layers;
    using Layouts;
    using Scrolls;
    using Selections;
    using Services;
    using Settings;
    using Swaps;

    public class PointViewVerticalLogic : PointViewLogic, IPointViewVerticalLogic 
    {
        private PointViewVerticalSettings viewSettings;

        protected PointViewVerticalLogic(
            IView view,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemBinding itemBinding,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemBinding, itemFactory)
        {
        }

        #region Property Injection

        public new IPointViewVerticalSelectionController ViewSelection
        {
            get
            {
                return (IPointViewVerticalSelectionController)base.ViewSelection;
            }
        }

        public new IPointViewVerticalScrollController ViewScroll
        {
            get
            {
                return (IPointViewVerticalScrollController)base.ViewScroll;
            }
        }

        public new IPointViewVerticalLayout ViewLayout
        {
            get { return (IPointViewVerticalLayout)base.ViewLayout; }
        }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointViewVerticalLayer>();
            this.viewSettings = viewLayer.ViewSettings;

            this.View[ViewState.View_Has_Focus] = this.View[ViewState.View_Has_Selection] = () => viewLayer.ViewSelection.HasSelected;
        }

        #endregion

        #region Events

        public event EventHandler ScrolledUp;

        public event EventHandler ScrolledDown;

        public event EventHandler MovedUp;

        public event EventHandler MovedDown;

        private void OnScrolledUp()
        {
            if (this.ScrolledUp != null)
            {
                this.ScrolledUp(this, EventArgs.Empty);
            }
        }

        private void OnScrolledDown()
        {
            if (this.ScrolledDown != null)
            {
                this.ScrolledDown(this, EventArgs.Empty);
            }
        }

        private void OnMovedUp()
        {
            if (this.MovedUp != null)
            {
                this.MovedUp(this, EventArgs.Empty);
            }
        }

        private void OnMovedDown()
        {
            if (this.MovedDown != null)
            {
                this.MovedDown(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Operations

        public void ScrollDown()
        {
            this.ViewScroll.MoveDown();

            this.OnScrolledDown();
        }

        public void ScrollUp()
        {
            this.ViewScroll.MoveUp();

            this.OnScrolledUp();
        }

        public void MoveUp()
        {
            if (this.viewSettings.ViewDirection == ViewDirection.Inverse)
            {
                this.ViewSelection.MoveDown();
            }
            else
            {
                this.ViewSelection.MoveUp();
            }

            this.OnMovedUp();
        }

        public void MoveDown()
        {
            if (this.viewSettings.ViewDirection == ViewDirection.Inverse)
            {
                this.ViewSelection.MoveUp();
            }
            else
            {
                this.ViewSelection.MoveDown();
            }

            this.OnMovedDown();
        }

        public void FastMoveDown()
        {
            for (var i = 0; i < this.viewSettings.ViewRowDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public void FastMoveUp()
        {
            for (var i = 0; i < this.viewSettings.ViewRowDisplay; i++)
            {
                this.MoveUp();
            }
        }

        #endregion Operations

        #region Update Input

        protected override void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            base.UpdateInputOfKeyboard(input, time);

            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = input.State.Keyboard;
                    if (keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.FastMoveUp();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.FastMoveDown();
                    }
                }
            }
        }

        protected override void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.MouseEnabled)
                {
                    var mouse = input.State.Mouse;
                    if (mouse.IsWheelScrolledUp)
                    {
                        this.ScrollUp();
                    }

                    if (mouse.IsWheelScrolledDown)
                    {
                        this.ScrollDown();
                    }
                }
            }
        }

        #endregion
    }
}