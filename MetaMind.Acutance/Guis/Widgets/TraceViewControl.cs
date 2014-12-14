namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Linq;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class TraceViewControl : GridControl
    {
        #region Constructors

        public TraceViewControl(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings, TraceItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Region    = new ViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
            this.ScrollBar = new ViewScrollBar(view, viewSettings, itemSettings, viewSettings.ScrollBarSettings);
        }

        #endregion Constructors

        #region Operations

        public void AddItem(TraceEntry entry)
        {
            var item = new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            View.Items.Add(item);
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();
            this.UpdateKeyboardMotion(gameTime);

            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // list management
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TraceCreateItem))
                    {
                        this.AddItem();

                        // auto select new item
                        this.Selection.Select(this.View.Items.Count - 1);
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TraceDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (this.View.Items.Count > 1)
                        {
                            // this will be called before item deletion
                            this.Selection.Select(this.View.Items.Count - 2);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TraceClearItem))
                    {
                        var notEmpty = this.View.Items.Count(item => !string.IsNullOrEmpty(item.ItemData.Name));
                        if (notEmpty > 0)
                        {
                            this.Selection.Select(notEmpty - 1);
                        }
                        else
                        {
                            this.Selection.Select(0);
                        }
                    }
                }
            }

            this.UpdateItemInput(gameTime);
        }

        protected override void UpdateViewFocus()
        {
            if (this.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                View.Disable(ViewState.View_Has_Focus);
            }
        }

        #endregion Update

        #region Configurations

        protected override Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}