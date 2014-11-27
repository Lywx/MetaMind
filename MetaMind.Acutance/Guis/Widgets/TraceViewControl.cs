namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements.Regions;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    using Microsoft.Xna.Framework;

    public class TraceViewControl : ViewControl2D
    {
        #region Constructors

        public TraceViewControl(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Region      = new ViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
            this.ScrollBar   = new ViewScrollBar(view, viewSettings, itemSettings, viewSettings.ScrollBarSettings);
            this.ItemFactory = new TraceItemFactory();
        }

        #endregion Constructors

        #region Public Properties

        public TraceItemFactory ItemFactory { get; protected set; }

        public ViewRegion Region { get; protected set; }

        public ViewScrollBar ScrollBar { get; protected set; }

        #endregion Public Properties

        #region Operations

        public void AddItem(TraceEntry entry)
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry));
        }

        public void AddItem()
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory));
        }

        public void MoveDown()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveDown();
        }

        public override void MoveLeft()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveLeft();
        }

        public override void MoveRight()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveRight();
        }

        public void MoveUp()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveUp();
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get { return this.View.IsEnabled(ViewState.Item_Editting); }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse
            // -----------------------------------------------------------------
            // region
            if (this.Active)
            {
                this.Region.UpdateInput(gameTime);
            }

            if (this.AcceptInput)
            {
                // mouse
                // ---------------------------------------------------------------------
                // scroll
                if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                {
                    this.ScrollBar.Trigger();
                    this.Scroll.MoveUp();
                }

                if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                {
                    this.Scroll.MoveDown();
                    this.ScrollBar.Trigger();
                }

                // keyboard
                // ---------------------------------------------------------------------
                // movement
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                {
                    this.MoveLeft();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    this.MoveRight();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                {
                    this.MoveUp();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                {
                    this.MoveDown();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SUp))
                {
                    for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
                    {
                        this.MoveUp();
                    }
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SDown))
                {
                    for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
                    {
                        this.MoveDown();
                    }
                }

                // escape
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                {
                    this.Selection.Clear();
                }

                // list management
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskCreateItem))
                {
                    this.AddItem();
                }
            }

            // item input
            // -----------------------------------------------------------------
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

        /// <remarks>
        /// All state change should be inside this methods. 
        /// </remarks>>
        /// <param name="gameTime"></param>
        public override void UpdateStructure(GameTime gameTime)
        {
            base          .UpdateStructure(gameTime);
            this.Region   .UpdateStructure(gameTime);
            this.ScrollBar.Update(gameTime);
        }

        protected override void UpdateViewFocus()
        {
            View.Enable(ViewState.View_Has_Focus); 
        }

        #endregion Update

        #region Configurations

        private Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }


        #endregion
    }
}