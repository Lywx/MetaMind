namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using Microsoft.Xna.Framework;

    public interface IConsciousness
    {
        bool HasAwaken { get; }

        bool IsAsleep { get; }

        bool IsAwake { get; }

        void Update(GameTime time);
    }
}