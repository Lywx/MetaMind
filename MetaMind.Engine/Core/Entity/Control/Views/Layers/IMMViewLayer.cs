namespace MetaMind.Engine.Core.Entity.Control.Views.Layers
{
    public interface IMMViewLayer : IMMViewComponent
    {
        T Get<T>() where T : class, IMMViewLayer;
    }
}