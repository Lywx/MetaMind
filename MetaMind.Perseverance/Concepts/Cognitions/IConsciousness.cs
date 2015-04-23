namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public interface IConsciousness
    {
        bool IsAsleep { get; }

        bool IsAwake { get; }

        void Awaken();

        void Sleep();

        void Update();
    }
}