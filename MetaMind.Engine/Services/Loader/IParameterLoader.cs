namespace MetaMind.Engine.Services.Loader
{
    public interface IParameterLoader<in T>
        where T : IParameter
    {
        void LoadParameter(T parameter);
    }
}