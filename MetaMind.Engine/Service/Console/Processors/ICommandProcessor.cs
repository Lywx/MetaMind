namespace MetaMind.Engine.Service.Console.Processors
{
    using Commands;

    public interface ICommandProcessor
    {
        string Process(string buffer);

        IConsoleCommand Match(string incomplete);
    }
}