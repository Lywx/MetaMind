namespace MetaMind.Testimony.Sessions
{
    using Concepts.Tests;
    using MetaMind.Testimony.Concepts.Cognitions;

    public interface ISessionData : Engine.Sessions.ISessionData
    {
        ICognition Cognition { get; }

        ITest Test { get; }
    }
}