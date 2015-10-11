namespace MetaMind.Engine.Entities.Controls.Item
{
    public static class MMViewItemStateExtension
    {
        public static bool Match(this MMViewItemState state, IMMViewItem item)
        {
            return item[state]();
        }
    }
}