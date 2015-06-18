namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewComponentOperations
    {
        T GetComponent<T>(string id) where T : class;
    }
}