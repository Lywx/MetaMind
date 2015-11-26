namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    using System;
    using System.Linq;
    using Item;

    public class MMViewLayout : MMViewControlComponent, IMMViewLayout
    {
        public MMViewLayout(IMMView view) : base(view) {}

        public void Sort(Func<IMMViewItem, dynamic> key)
        {
            this.ItemsWrite = this.View.Items.OrderBy(key).ToList();

            // Re-label items' id based on new order
            this.ItemsWrite.ForEach(
                item =>
                item.ItemLogic.ItemLayout.Id = this.ItemsWrite.IndexOf(item));
        }
    }
}
