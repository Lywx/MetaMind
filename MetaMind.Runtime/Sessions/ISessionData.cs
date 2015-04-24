namespace MetaMind.Runtime.Sessions
{
    using MetaMind.Runtime.Concepts.Cognitions;

    public interface ISessionData : Engine.Sessions.ISessionData
    {
        ICognition Cognition { get; } 
    }
}