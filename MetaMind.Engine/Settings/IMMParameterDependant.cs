namespace MetaMind.Engine.Services.IO
{
    public interface IMMParameterDependant<in T>
        where T : IMMParameter
    {
        void LoadParameter(T parameter);
    }
}