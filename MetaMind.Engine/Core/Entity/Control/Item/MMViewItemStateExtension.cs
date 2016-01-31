namespace MetaMind.Engine.Core.Entity.Control.Item
{
    public static class MMViewItemStateExtension
    {
        public static bool Match(this MMViewItemState state, IMMViewItem item)
        {
            return item[state]();
        }
    }
}