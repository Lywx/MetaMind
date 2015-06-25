namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    public interface IViewItemDataModel : IViewItemComponent
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}