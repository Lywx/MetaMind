namespace MetaMind.Engine.Session
{
    public interface ISession<out TData, out TController>
        where TData : ISessionData, new()
        where TController : ISessionController<TData>, new()
    {
        TData Data { get; }

        TController Controller { get; }

        void Save();

        void Update();
    }
}