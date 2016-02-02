namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using System;
    using Backend.Input;
    using Backend.Input.Keyboard;
    using Item.Data;
    using Item.Factories;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class MMPointView2DController : MMPointViewHorizontalController, IMMPointView2DController 
    {
        private PointView2DSettings viewSettings;

        public MMPointView2DController(
            IMMView view,
            IMMViewScrollController viewScroll,
            IMMViewSelectionController viewSelection,
            IMMViewSwapController viewSwap,
            IMMViewLayout viewLayout,
            IMMViewBinding binding,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        #region Indirect Dependency

        protected PointView2DSettings ViewSettings
        {
            get { return this.viewSettings; }
        }

        #endregion

        #region Layer Injecetion 

        public new IMMPointView2DLayout ViewLayout
        {
            get
            {
                return (IMMPointView2DLayout)base.ViewLayout;
            }
        }

        public new IMMPointView2DSelectionController ViewSelection
        {
            get
            {
                return (IMMPointView2DSelectionController)base.ViewSelection;
            }
        }

        public new IMMPointView2DScrollController ViewScroll
        {
            get
            {
                return (IMMPointView2DScrollController)base.ViewScroll;
            }
        }

        IMMPointViewVerticalSelectionController IMMPointViewVerticalController.ViewSelection
        {
            get
            {
                return this.ViewSelection;
            }
        }

        IMMPointViewVerticalScrollController IMMPointViewVerticalController.ViewScroll
        {
            get
            {
                return this.ViewScroll;
            }
        }

        IMMPointViewVerticalLayout IMMPointViewVerticalController.ViewLayout
        {
            get
            {
                return this.ViewLayout;
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

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            var viewLayer = this.GetViewLayer<MMPointView2DLayer>();
            this.viewSettings = viewLayer.ViewSettings;
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

        public virtual void MoveDown()
        {
            this.ViewSelection.MoveDown();

            this.OnMovedDown();
        }

        public virtual void MoveUp()
        {
            this.ViewSelection.MoveUp();

            this.OnMovedUp();
        }

        public virtual void FastMoveDown()
        {
            for (var i = 0; i < this.ViewSettings.ViewRowDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public virtual void FastMoveUp()
        {
            for (var i = 0; i < this.ViewSettings.ViewRowDisplay; i++)
            {
                this.MoveUp();
            }
        }

        #endregion Operations

        #region Update Input

        protected override void UpdateMouseInput(GameTime time)
        {
            if (this.View[MMViewState.View_Is_Inputting]())
            {
                if (this.ViewSettings.MouseEnabled)
                {
                    var mouse = this.GlobalInput.State.Mouse;
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

        protected override void UpdateKeyboardInput(GameTime time)
        {
            if (this.View[MMViewState.View_Is_Inputting]())
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = this.GlobalInput.State.Keyboard;
                    if (keyboard.IsActionTriggered(MMInputActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.FastUp))
                    {
                        this.FastMoveUp();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.FastDown))
                    {
                        this.FastMoveDown();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.FastRight))
                    {
                        this.FastMoveRight();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.Escape))
                    {
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        #endregion Update
    }
}