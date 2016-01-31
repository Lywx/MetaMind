namespace MetaMind.Engine.Core.Entity.Control.Item.Layers
{
    public interface IMMViewItemLayer : IMMViewItemControllerComponent
    {
        T Get<T>() where T : class, IMMViewItemLayer;
    }
}