namespace MetaMind.Engine.Entities.Controls.Item.Data
{
    public interface IMMViewItemDataModel : IMMViewItemControllerComponent
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}