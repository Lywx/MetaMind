namespace MetaMind.Unity
{
    using Console.Commands;
    using Engine;

    public class UnityEngineCompositor : GameEngineCompositor
    {
        public override void Configure(GameEngine engine)
        {
            base.Configure(engine);

            var console = engine.Interop.Console;
            console.AddCommand(new ListCommand());
        }
    }
}
