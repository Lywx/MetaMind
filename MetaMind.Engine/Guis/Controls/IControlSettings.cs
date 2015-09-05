namespace MetaMind.Engine.Guis.Widgets
{
    public interface IControlSettings
    {
        T Get<T>(string id);
    }
}