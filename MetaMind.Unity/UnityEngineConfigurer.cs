namespace MetaMind.Unity
{
    using Console.Uniutils;
    using Engine;

    public class UnityEngineConfigurer : IGameEngineConfigurer
    {
        public void Configure(GameEngine engine)
        {
            // Console
            var console = engine.Interop.Console;
            console.AddCommand(new ListCommand());
        }
    }
}