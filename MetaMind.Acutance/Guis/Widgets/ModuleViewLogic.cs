namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ModuleViewLogic : PointGridLogic
    {
        #region Constructors

        public ModuleViewLogic(IView view, ModuleViewSettings viewSettings, ModuleItemSettings itemSettings, ModuleItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.View[ViewState.View_Has_Focus] = this.Region[RegionState.Region_Has_Focus];
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Module entry)
        {
            var item = new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            this.View.ViewItems.Add(item);
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

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);

            if (this.View[ViewState.View_Is_Inputting]())
            {
                // Keyboard
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.ModuleDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (View.ViewItems.Count > 1)
                        {
                            // this will be called before item deletion
                            if (this.ViewSelection.SelectedId != null && 
                                this.ViewSelection.SelectedId > View.ViewItems.Count - 2)
                            {
                                this.ViewSelection.Select(View.ViewItems.Count - 2);
                            }
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.ModuleClearItem))
                    {
                        var notEmpty = this.View.ViewItems.Count;
                        if (notEmpty > 0)
                        {
                            this.ViewSelection.Select(notEmpty - 1);
                        }
                        else
                        {
                            this.ViewSelection.Select(0);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.ModuleSortItem))
                    {
                        this.SortItems(PointViewSortMode.Name);
                    }
                }
            }

            this.UpdateInputOfItems(input, time);
        }

        #endregion Update

        #region Configurations

        protected override Rectangle RegionBounds()
        {
            return new Rectangle(
                this.viewSettings.PointStart.X,
                this.viewSettings.PointStart.Y,
                this.viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                this.viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}