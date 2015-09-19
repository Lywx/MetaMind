namespace MetaMind.Engine.Gui.Control.Item.Data
{
    public interface IViewItemDataModel : IViewItemComponent
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}