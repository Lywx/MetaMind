namespace MetaMind.Engine.Gui.Control
{
    public interface IControlSettings
    {
        T Get<T>(string id);
    }
}