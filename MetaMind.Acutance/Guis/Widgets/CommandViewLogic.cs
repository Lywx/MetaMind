namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class CommandViewLogic : PointGridLogic
    {
        #region Constructors

        public CommandViewLogic(IView view, CommandViewSettings viewSettings, CommandItemSettings itemSettings, CommandItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Command entry)
        {
            var item = new ViewItem(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.ViewItems.Add(item);
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

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);

            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (ViewSettings.KeyboardEnabled)
                {
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommandClearItem))
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

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommandDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (View.ViewItems.Count > 1)
                        {
                            // this will be commanded before item deletion
                            this.ViewSelection.Select(View.ViewItems.Count - 2);
                        }
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommandSortItem))
                    {
                        this.SortItems(PointViewSortMode.Name);
                    }
                }
            }

            this.UpdateInputOfItems(input, time);
        }

        #region Configurations

        protected override Rectangle RegionBounds()
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