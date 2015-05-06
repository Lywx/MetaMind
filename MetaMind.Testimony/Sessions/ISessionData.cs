namespace MetaMind.Testimony.Sessions
{
    using MetaMind.Testimony.Concepts.Cognitions;

    public interface ISessionData : Engine.Sessions.ISessionData
    {
        ICognition Cognition { get; } 
    }
}