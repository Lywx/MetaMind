namespace MetaMind.Engine.Core.Entity.Control.Item.Controllers
{
    using System.Diagnostics;

    public static class MMViewItemControllerExtension
    {
        public static void OpenPath(this MMViewItemController itemLogic)
        {
            Process.Start(itemLogic.Item.ItemData.Path);
        }

        public static void SelectPath(this MMViewItemController itemLogic)
        {
            var argument = @"/select, " + itemLogic.Item.ItemData.Path;
            Process.Start("explorer.exe", argument);
        }
    }
}