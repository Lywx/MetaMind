namespace MetaMind.Unity.Console.Uniutils
{
    using System.Linq;
    using Engine.Console.Commands;

    internal class ListCommand : IConsoleCommand
    {
        public string Name => "unity-list";

        public string Description => "Lists tests, operations and representation";

        public string Execute(string[] arguments)
        {
            var listing = (from test in Unity.Session.Data.Test.AllCollection where !test.Evaluation.ResultPassed select test.Name).ToList();

            return string.Join("\n", listing);
        }
    }
}