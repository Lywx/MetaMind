namespace MetaMind.Session.Sessions
{
    using Concepts.Cognitions;
    using Concepts.Operations;
    using Concepts.Tests;

    public interface ISessionData : Engine.Sessions.IMMSessionData
    {
        ICognition Cognition { get; }

        ITest Test { get; }

        IOperationDescription Operation { get; }
    }
}