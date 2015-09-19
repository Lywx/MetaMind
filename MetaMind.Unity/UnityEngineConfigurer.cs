namespace MetaMind.Unity
{
    using Console.Commands;
    using Engine;

    public class UnityEngineConfigurer : IGameEngineConfigurer
    {
        public void Configure(GameEngine engine)
        {
            var console = engine.Interop.Console;
            console.AddCommand(new ListCommand());
        }
    }
}
