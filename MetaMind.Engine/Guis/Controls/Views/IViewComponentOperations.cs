namespace MetaMind.Engine.Guis.Controls.Views
{
    public interface IViewComponentOperations
    {
        T GetComponent<T>(string id) where T : class;
    }
}