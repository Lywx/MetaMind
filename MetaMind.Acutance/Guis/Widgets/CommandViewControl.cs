namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class CommandViewControl : PointGridControl
    {
        #region Constructors

        public CommandViewControl(IView view, CommandViewSettings viewSettings, CommandItemSettings itemSettings, CommandItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Command entry)
        {
            var item = new ViewItemExchangeless(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.Items.Add(item);
        }

        public override void SortItems(PointViewSortMode sortMode)
        {
            base.SortItems(sortMode);

            switch (sortMode)
            {
                case PointViewSortMode.Name:
                    {
                        ViewSettings.Source.Sort(CommandSortMode.Name);
                    }

                    break;
            }
        }


        #endregion

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            this.UpdateRegionClick(gameInput, gameTime);
            this.UpdateMouseScroll(gameInput);
            this.UpdateKeyboardMotion(gameInput, gameTime);

            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CommandClearItem))
                    {
                        var notEmpty = View.Items.Count;
                        if (notEmpty > 0)
                        {
                            this.Selection.Select(notEmpty - 1);
                        }
                        else
                        {
                            this.Selection.Select(0);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CommandDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (View.Items.Count > 1)
                        {
                            // this will be commanded before item deletion
                            this.Selection.Select(View.Items.Count - 2);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CommandSortItem))
                    {
                        this.SortItems(PointViewSortMode.Name);
                    }
                }
            }

            this.UpdateItemInput(gameInput, gameTime);
        }

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