namespace MetaMind.Engine.Gui.Controls.Views
{
    public interface IMMViewComponentOperations
    {
        T GetComponent<T>(string id) where T : class;
    }
}