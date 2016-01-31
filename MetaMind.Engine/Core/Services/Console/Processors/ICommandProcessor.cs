namespace MetaMind.Engine.Core.Services.Console.Processors
{
    using Commands;

    public interface ICommandProcessor
    {
        string Process(string buffer);

        IConsoleCommand Match(string incomplete);
    }
}