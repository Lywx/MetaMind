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

    public class MMPointViewVerticalController : MMPointViewController, IMMPointViewVerticalController 
    {
        private PointViewVerticalSettings viewSettings;

        protected MMPointViewVerticalController(IMMView view, IMMViewScrollController viewScroll, IMMViewSelectionController viewSelection, IMMViewSwapController viewSwap, IMMViewLayout viewLayout, IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        #region Property Injection

        public new IMMPointViewVerticalSelectionController ViewSelection => (IMMPointViewVerticalSelectionController)base.ViewSelection;

        public new IMMPointViewVerticalScrollController ViewScroll => (IMMPointViewVerticalScrollController)base.ViewScroll;

        public new IMMPointViewVerticalLayout ViewLayout => (IMMPointViewVerticalLayout)base.ViewLayout;

        #endregion

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            var viewLayer = this.GetViewLayer<MMPointViewVerticalLayer>();
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

        #endregion

        #region Events

        public event EventHandler ScrolledUp;

        public event EventHandler ScrolledDown;

        public event EventHandler MovedUp;

        public event EventHandler MovedDown;

        private void OnScrolledUp()
        {
            this.ScrolledUp?.Invoke(this, EventArgs.Empty);
        }

        private void OnScrolledDown()
        {
            this.ScrolledDown?.Invoke(this, EventArgs.Empty);
        }

        private void OnMovedUp()
        {
            this.MovedUp?.Invoke(this, EventArgs.Empty);
        }

        private void OnMovedDown()
        {
            this.MovedDown?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Operations

        public virtual void ScrollDown()
        {
            this.ViewScroll.MoveDown();

            this.OnScrolledDown();
        }

        public virtual void ScrollUp()
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

        protected override void UpdateKeyboardInput(GameTime time)
        {
            base.UpdateKeyboardInput(time);

            if (this.View[MMViewState.View_Is_Inputting]())
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

        protected override void UpdateMouseInput(GameTime time)
        {
            if (this.View[MMViewState.View_Is_Inputting]())
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