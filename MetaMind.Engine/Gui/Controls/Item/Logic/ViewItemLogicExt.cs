namespace MetaMind.Engine.Gui.Controls.Item.Logic
{
    using System.Diagnostics;

    public static class ViewItemLogicExt
    {
        public static void OpenPath(this ViewItemLogic itemLogic)
        {
            Process.Start(itemLogic.Item.ItemData.Path);
        }

        public static void SelectPath(this ViewItemLogic itemLogic)
        {
            var argument = @"/select, " + itemLogic.Item.ItemData.Path;
            Process.Start("explorer.exe", argument);
        }
    }
}