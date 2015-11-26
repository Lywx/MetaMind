namespace MetaMind.Engine.Services.Loader
{
    public interface IParameterDependant<in T>
        where T : IParameter
    {
        void LoadParameter(T parameter);
    }
}