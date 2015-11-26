namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    public interface IMMViewItemLayer : IMMViewItemControllerComponent
    {
        T Get<T>() where T : class, IMMViewItemLayer;
    }
}