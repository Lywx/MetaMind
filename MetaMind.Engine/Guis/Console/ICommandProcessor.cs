namespace MetaMind.Engine.Guis.Console
{
    using Commands;

    public interface ICommandProcessor
    {
        string Process(string buffer);

        IConsoleCommand Match(string incomplete);
    }
}