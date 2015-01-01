namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;

    public class CommandViewSettings : TraceViewSettings
    {
        public Commandlist Source;

        public CommandViewSettings(Commandlist source)
        {
            this.Source = source;
        }
    }
}