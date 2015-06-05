namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Components.Inputs;
    using Items.Factories;
    using Layers;
    using Layouts;
    using Scrolls;
    using Selections;
    using Services;
    using Settings;
    using Swaps;

    public class PointViewVerticalLogic<TData> : PointViewLogic<TData>, IPointViewVerticalLogic
    {
        private PointViewVerticalSettings viewSettings;

        protected PointViewVerticalLogic(
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

        #region Property Injection

        public new IPointViewVerticalSelectionController ViewSelection
        {
            get
            {
                return (IPointViewVerticalSelectionController)base.ViewSelection;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (base.ViewSelection != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewSelection = value;
            }
        }

        public new IPointViewVerticalScrollController ViewScroll
        {
            get
            {
                return (IPointViewVerticalScrollController)base.ViewScroll;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (base.ViewScroll != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewScroll = value;
            }
        }

        public new IPointViewVerticalLayout ViewLayout
        {
            get { return (IPointViewVerticalLayout)base.ViewLayout; }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (base.ViewLayout != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewLayout = value;
            }
        }


        public new IPointViewVerticalSwapController ViewSwap
        {
            get
            {
                return (IPointViewVerticalSwapController)base.ViewSwap;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (base.ViewSwap != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewSwap = value;
            }
        }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointViewVerticalLayer>();
            this.viewSettings = viewLayer.ViewSettings;

            this.View[ViewState.View_Has_Focus] =
                this.View[ViewState.View_Has_Selection] =
                    () => viewLayer.ViewSelection.HasSelected;
        }

        #endregion

        #region Operations

        public virtual void ScrollDown()
        {
            this.ViewScroll.MoveDown();
        }

        public virtual void ScrollUp()
        {
            this.ViewScroll.MoveUp();
        }

        public virtual void MoveUp()
        {
            if (this.viewSettings.Direction == ViewDirection.Inverse)
            {
                this.ViewSelection.MoveDown();
            }
            else
            {
                this.ViewSelection.MoveUp();
            }
        }

        public virtual void MoveDown()
        {
            if (this.viewSettings.Direction == ViewDirection.Inverse)
            {
                this.ViewSelection.MoveUp();
            }
            else
            {
                this.ViewSelection.MoveDown();
            }
        }

        public virtual void FastMoveDown()
        {
            for (var i = 0; i < this.viewSettings.RowNumDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public virtual void FastMoveUp()
        {
            for (var i = 0; i < this.viewSettings.RowNumDisplay; i++)
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
                    // Movement
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