namespace MetaMind.Engine.Gui.Controls.Item.Data
{
    public interface IViewItemDataModel : IViewItemComponent
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}