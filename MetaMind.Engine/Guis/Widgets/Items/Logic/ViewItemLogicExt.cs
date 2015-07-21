namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using System.Diagnostics;
    using System.IO;

    public static class ViewItemLogicExt
    {
        public static void OpenPath(this ViewItemLogic itemLogic)
        {
            Process.Start(itemLogic.Item.ItemData.Path);
        }

        public static void OpenFolderPath(this ViewItemLogic itemLogic)
        {
            var directoryPath = Path.GetDirectoryName(itemLogic.Item.ItemData.Path);
            Process.Start(directoryPath);
        }
    }
}