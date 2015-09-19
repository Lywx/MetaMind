namespace MetaMind.Engine.Gui.Control.Views
{
    public interface IViewComponentOperations
    {
        T GetComponent<T>(string id) where T : class;
    }
}