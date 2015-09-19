namespace MetaMind.Unity.Sessions
{
    using Concepts.Cognitions;
    using Concepts.Operations;
    using Concepts.Tests;

    public interface ISessionData : Engine.Session.ISessionData
    {
        ICognition Cognition { get; }

        ITest Test { get; }

        IOperationDescription Operation { get; }
    }
}