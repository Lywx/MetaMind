namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class CommandItemDataControl : ViewItemDataControl
    {
        public CommandItemDataControl(IViewItem item)
            : base(item)
        {
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            if (ItemData.State == CommandState.Running || 
                ItemData.State == CommandState.Terminated)
            {
                ItemControl.DeleteIt();
            }
        }
    }
}