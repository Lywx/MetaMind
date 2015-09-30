namespace MetaMind.Engine.Service.Loader
{
    public interface IParameterLoader<in T>
        where T : IParameter
    {
        void LoadParameter(T parameter);
    }
}