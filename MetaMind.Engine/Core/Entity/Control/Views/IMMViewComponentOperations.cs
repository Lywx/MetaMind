namespace MetaMind.Engine.Core.Entity.Control.Views
{
    using Layers;

    public interface IMMViewComponentOperations : IMMViewLayerOperations 
    {
        T GetComponent<T>(string id) where T : class;
    }
}