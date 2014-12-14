namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class TraceItemDataControl : ViewItemDataControl
    {
        public TraceItemDataControl(IViewItem item)
            : base(item)
        {
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            ItemData.Update();
        }
    }
}