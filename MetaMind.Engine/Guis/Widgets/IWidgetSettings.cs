namespace MetaMind.Engine.Guis.Widgets
{
    public interface IWidgetSettings
    {
        T Get<T>(string id);
    }
}