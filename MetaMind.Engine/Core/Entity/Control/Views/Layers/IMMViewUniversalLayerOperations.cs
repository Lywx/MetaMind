namespace MetaMind.Engine.Core.Entity.Control.Views.Layers
{
    public interface IMMViewUniversalLayerOperations 
    {
        T GetViewLayer<T>() where T : class, IMMViewLayer;
    }
}