namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;

    public class CommandViewSettings : TraceViewSettings
    {
        public ICommandlist Source;

        public CommandViewSettings(ICommandlist source)
        {
            this.Source = source;
        }
    }
}