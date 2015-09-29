namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Frames;
    using Microsoft.Xna.Framework;
    using Tests;

    public class OperationItemSettings : TestItemSettings
    {
        public OperationItemSettings()
        {
            var statusFrameSettings = this.Get<ViewItemVisualSettings>("StatusFrame");
            statusFrameSettings.Size = new Point(128, 52);
        }
    }
}