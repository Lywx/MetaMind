namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    public interface IMMViewItemLayer : IMMViewItemController
    {
        T Get<T>() where T : class, IMMViewItemLayer;
    }
}