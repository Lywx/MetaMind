namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using Engine.Entities.Controls.Item;
    using Microsoft.Xna.Framework;
    using Tests;

    public class OperationItemSettings : TestItemSettings
    {
        public OperationItemSettings()
        {
            var statusFrameSettings = this.Get<MMViewItemRenderSettings>("StatusFrame");
            statusFrameSettings.Size = new Point(128, 52);
        }
    }
}