namespace MetaMind.Engine.Guis.Controls.Items.Data
{
    public interface IViewItemDataModel : IViewItemComponent
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}