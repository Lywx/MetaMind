namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;

    public class ModuleViewSettings : TraceViewSettings
    {
        public IModulelist Source;

        public ModuleViewSettings(IModulelist source)
        {
            this.Source = source;
        }
    }
}