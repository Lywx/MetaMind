namespace MetaMind.Engine.Core.Entity.Control.Item.Data
{
    public interface IMMViewItemDataModel : IMMViewItemControllerComponent
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}