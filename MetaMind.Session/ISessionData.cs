namespace MetaMind.Session.Sessions
{
    using Operations;
    using Runtime;
    using Tests;

    public interface ISessionData : Engine.Sessions.IMMSessionData
    {
        ICognition Cognition { get; }

        ITest Test { get; }

        IOperationDescription Operation { get; }
    }
}