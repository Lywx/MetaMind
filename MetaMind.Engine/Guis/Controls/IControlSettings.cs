namespace MetaMind.Engine.Guis.Controls
{
    public interface IControlSettings
    {
        T Get<T>(string id);
    }
}