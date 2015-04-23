namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;

    public interface IConsciousnessAwake : IConsciousnessState
    {
        TimeSpan AwakeSpan { get; }
    }
}