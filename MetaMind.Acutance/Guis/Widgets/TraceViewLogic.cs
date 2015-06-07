namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class TraceViewLogic : PointGridLogic
    {
        #region Constructors

        public TraceViewLogic(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings, TraceItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Trace entry)
        {
            var item = new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            View.ViewItems.Add(item);
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);

            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // list management
                    if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.TraceCreateItem))
                    {
                        this.AddItem();

                        // auto select new item
                        this.ViewSelection.Select(this.View.ViewItems.Count - 1);
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.TraceDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (this.View.ViewItems.Count > 1)
                        {
                            // this will be called before item deletion
                            this.ViewSelection.Select(this.View.ViewItems.Count - 2);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.TraceClearItem))
                    {
                        var notEmpty = View.ViewItems.Count;
                        if (notEmpty > 0)
                        {
                            this.ViewSelection.Select(notEmpty - 1);
                        }
                        else
                        {
                            this.ViewSelection.Select(0);
                        }
                    }
                }
            }

            this.UpdateInputOfItems(input, time);
        }

        protected override void UpdateViewFocus()
        {
            if (this.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                View[View.State.View_Has_Focus] = () => true;
            }
            else
            {
                View.Disable(ViewState.View_Has_Focus);
            }
        }

        #endregion Update

        #region Configurations

        protected override Rectangle RegionBounds()
        {
            return new Rectangle(
                viewSettings.PointStart.X,
                viewSettings.PointStart.Y,
                viewSettings.ViewColumnDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                viewSettings.ViewRowDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}