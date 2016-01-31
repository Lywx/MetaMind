namespace MetaMind.Session
{
    using Engine.Core.Sessions;
    using Model.Runtime;
    using Operations;
    using Tests;

    public interface IMMSessionGameData : IMMSessionData
    {
        ICognition Cognition { get; }

        ITest Test { get; }

        IOperationDescription Operation { get; }
    }
}