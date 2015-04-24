namespace MetaMind.Engine.Sessions
{
    public interface ISession<out TData>
        where TData : ISessionData, new()
    {
        TData Data { get; }

        void Save();

        void Update();
    }
}