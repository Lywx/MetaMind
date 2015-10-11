namespace MetaMind.Engine.Entities.Controls.Views.Layers
{
    public interface IMMViewLayer : IMMViewComponent
    {
        T Get<T>() where T : class, IMMViewLayer;
    }
}