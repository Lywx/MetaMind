namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using System;
    using Extensions;
    using Item.Factories;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class MMPointViewHorizontalController : MMPointViewController, IMMPointViewHorizontalController 
    {
        private PointViewHorizontalSettings viewSettings;

        protected MMPointViewHorizontalController(IMMView view, IMMViewScrollController viewScroll, IMMViewSelectionController viewSelection, IMMViewSwapController viewSwap, IMMViewLayout viewLayout, IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        public override void Initialize()
        {
            var viewLayer = this.GetViewLayer<MMPointViewHorizontalLayer>();
            this.viewSettings = viewLayer.ViewSettings;

            try
            {
                var expression = this.View[MMViewState.View_Has_Selection].ToExpression();

                if (expression.Equals<bool>(new Func<bool>(() => false).ToExpression()))
                {
                    this.View[MMViewState.View_Has_Selection] = () => viewLayer.ViewSelection.HasSelected;
                }
            }
            catch (ArgumentException)
            {
                // Must be already assigned to instance function, so it is desirable not to set the delegate
            }

            try
            {
                var expression = this.View[MMViewState.View_Has_Focus].ToExpression();

                if (expression.Equals<bool>(new Func<bool>(() => false).ToExpression()))
                {
                    this.View[MMViewState.View_Has_Focus] = this.View[MMViewState.View_Has_Selection];
                }
            }
            catch (ArgumentException)
            {
                // Must be already assigned to instance function, so it is desirable not to set the delegate
            }
        }

        #region View Logic Property Injection

        public new IMMPointViewHorizontalSelectionController ViewSelection => (IMMPointViewHorizontalSelectionController)base.ViewSelection;

        public new IMMPointViewHorizontalScrollController ViewScroll => (IMMPointViewHorizontalScrollController)base.ViewScroll;

        public new IMMPointViewHorizontalLayout ViewLayout => (IMMPointViewHorizontalLayout)base.ViewLayout;

        #endregion

        #region Events

        public event EventHandler ScrolledLeft;

        public event EventHandler ScrolledRight;

        public event EventHandler MovedLeft;

        public event EventHandler MovedRight;

        private void OnScrolledLeft()
        {
            this.ScrolledLeft?.Invoke(this, EventArgs.Empty);
        }

        private void OnScrolledRight()
        {
            this.ScrolledRight?.Invoke(this, EventArgs.Empty);
        }

        private void OnMovedLeft()
        {
            this.MovedLeft?.Invoke(this, EventArgs.Empty);
        }

        private void OnMovedRight()
        {
            this.MovedRight?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Operations

        public virtual void ScrollLeft()
        {
            this.ViewScroll.MoveLeft();

            this.OnScrolledLeft();
        }

        public virtual void ScrollRight()
        {
            this.ViewScroll.MoveRight();

            this.OnScrolledRight();
        }

        public virtual void MoveLeft()
        {
            if (this.viewSettings.ViewDirection == ViewDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveRight();
            }
            else
            {
                this.ViewSelection.MoveLeft();
            }

            this.OnMovedLeft();
        }

        public virtual void MoveRight()
        {
            if (this.viewSettings.ViewDirection == ViewDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveLeft();
            }
            else
            {
                this.ViewSelection.MoveRight();
            }

            this.OnMovedRight();
        }

        public void FastMoveLeft()
        {
            for (var i = 0; i < this.viewSettings.ViewColumnDisplay; i++)
            {
                this.MoveLeft();
            }
        }

        public void FastMoveRight()
        {
            for (var i = 0; i < this.viewSettings.ViewColumnDisplay; i++)
            {
                this.MoveRight();
            }
        }

        #endregion Operations

        #region Update Input

        protected override void UpdateKeyboardInput(GameTime time)
        {
            base.UpdateKeyboardInput(time);

            if (this.View[MMViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = input.State.Keyboard;
                    if (keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveRight();
                    }
                }
            }
        }

        protected override void UpdateMouseInput(GameTime time)
        {
            if (this.View[MMViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.MouseEnabled)
                {
                    var mouse = input.State.Mouse;
                    if (mouse.IsWheelScrolledUp)
                    {
                        this.ScrollLeft();
                    }

                    if (mouse.IsWheelScrolledDown)
                    {
                        this.ScrollRight();
                    }
                }
            }
        }

        #endregion
    }
}