namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Linq;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class CallViewControl : GridControl
    {
        #region Constructors

        public CallViewControl(IView view, CallViewSettings viewSettings, CallItemSettings itemSettings, CallItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        public void AddItem(string name, string path, int minutes)
        {
            var callItemFactory = ItemFactory as CallItemFactory;
            if (callItemFactory != null)
            {
                var entry = callItemFactory.CreateData(name, path, minutes);
                this.AddItem(entry);
            }
        }

        public void AddItem(CallEntry entry)
        {
            var item = new ViewItemExchangeless(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.Items.Add(item);
        }

        public override void SortItems(ViewSortMode sortMode)
        {
            base.SortItems(sortMode);

            switch (sortMode)
            {
                case ViewSortMode.Name:
                    {
                        ViewSettings.Source.Sort(CallSortMode.Name);
                    }

                    break;
            } 
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();
            this.UpdateKeyboardMotion(gameTime);

            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CallClearItem))
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

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CallDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (View.Items.Count > 1)
                        {
                            // this will be called before item deletion
                            this.Selection.Select(View.Items.Count - 2);
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CallSortItem))
                    {
                        this.SortItems(ViewSortMode.Name);
                    }
                }
            }

            this.UpdateItemInput(gameTime);
        }

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