namespace MetaMind.Engine.Entities.Controls.Item.Data
{
    public interface IMMViewItemDataModel : IMMViewItemController
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}