namespace MetaMind.Session
{
    using Engine.Core.Services.IO;

    public class MMSessionPlainConfigurationRoot : IMMPlainConfigurationFileLoader
    {
        public string ConfigurationFilename => "Session.ini";
    }
}
