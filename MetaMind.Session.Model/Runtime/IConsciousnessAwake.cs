namespace MetaMind.Session.Model.Runtime
{
    using System;

    public interface IConsciousnessAwake : IConsciousnessState
    {
        TimeSpan AwakeSpan { get; }
    }
}