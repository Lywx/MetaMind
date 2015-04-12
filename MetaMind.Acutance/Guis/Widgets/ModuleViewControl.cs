namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class ModuleViewControl : PointGridControl
    {
        #region Constructors

        public ModuleViewControl(IView view, ModuleViewSettings viewSettings, ModuleItemSettings itemSettings, ModuleItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Module entry)
        {
            var item = new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            this.View.Items.Add(item);
        }

        public override void SortItems(PointViewSortMode sortMode)
        {
            base.SortItems(sortMode);

            switch (sortMode)
            {
                case PointViewSortMode.Name:
                    {
                        ViewSettings.Source.Sort(ModuleSortMode.Name);
                    }

                    break;
            } 
        }

        #endregion Operations

        #region Update

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            this.UpdateRegionClick(gameInput, gameTime);
            this.UpdateMouseScroll(gameInput);
            this.UpdateKeyboardMotion(gameInput, gameTime);

            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (View.Items.Count > 1)
                        {
                            // this will be called before item deletion
                            if (this.Selection.SelectedId != null && 
                                this.Selection.SelectedId > View.Items.Count - 2)
                            {
                                this.Selection.Select(View.Items.Count - 2);
                            }
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleClearItem))
                    {
                        var notEmpty = this.View.Items.Count;
                        if (notEmpty > 0)
                        {
                            this.Selection.Select(notEmpty - 1);
                        }
                        else
                        {
                            this.Selection.Select(0);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleSortItem))
                    {
                        this.SortItems(PointViewSortMode.Name);
                    }
                }
            }

            this.UpdateItemInput(gameInput, gameTime);
        }

        protected override void UpdateViewFocus()
        {
            if (this.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                this.View.Disable(ViewState.View_Has_Focus);
            }
        }

        #endregion Update

        #region Configurations

        protected override Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.PointStart.X,
                viewSettings.PointStart.Y,
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}