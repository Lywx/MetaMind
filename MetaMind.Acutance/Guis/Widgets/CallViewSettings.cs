namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;

    public class CallViewSettings : TraceViewSettings
    {
        public Calllist Source;

        public CallViewSettings(Calllist source)
        {
            this.Source = source;
        }
    }
}