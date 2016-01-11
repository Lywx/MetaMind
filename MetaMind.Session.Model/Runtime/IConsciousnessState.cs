namespace MetaMind.Session.Model.Runtime
{
    public interface IConsciousnessState
    {
        IConsciousnessState UpdateState(Consciousness consciousness);
    }
}