namespace MetaMind.Engine.Entities.Controls.Views.Layers
{
    public interface IMMViewUniversalLayerOperations 
    {
        T GetViewLayer<T>() where T : class, IMMViewLayer;
    }
}