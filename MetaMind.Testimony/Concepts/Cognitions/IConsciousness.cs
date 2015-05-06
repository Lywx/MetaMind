namespace MetaMind.Testimony.Concepts.Cognitions
{
    public interface IConsciousness
    {
        #region State Data

        bool IsAsleep { get; }

        bool IsAwake { get; }

        IConsciousnessState State { get; }

        #endregion

        #region Operations

        void Awaken();

        void Sleep();

        #endregion

        void Update();
    }
}