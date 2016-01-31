namespace MetaMind.Session.Model.Settings
{
    using Engine.Core.Services.IO;
    using Engine.Core.Settings;

    public class ModelSettings : IMMPlainConfigurationFileLoader, IMMParameter
    {
        public int DialogueLength { set; get; }

        public string ConfigurationFilename => "Session.Model.ini";
    }
}
