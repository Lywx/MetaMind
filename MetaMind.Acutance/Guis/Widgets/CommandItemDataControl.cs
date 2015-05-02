namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Data;

    using Microsoft.Xna.Framework;

    public class CommandItemDataControl : ViewItemDataModifier
    {
        public CommandItemDataControl(IViewItem item)
            : base(item)
        {
        }

        public override void Update(GameTime time)
        {
            if (ItemData.State == CommandState.Running || 
                ItemData.State == CommandState.Terminated)
            {
                this.ItemLogic.DeleteIt();
            }
        }
    }
}