namespace MetaMind.Engine.Entities.Controls.Views
{
    using Layers;

    public interface IMMViewComponentOperations : IMMViewLayerOperations 
    {
        T GetComponent<T>(string id) where T : class;
    }
}