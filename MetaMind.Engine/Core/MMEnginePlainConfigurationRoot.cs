namespace MetaMind.Engine.Core
{
    using Services.IO;

    /// <summary>
    /// This class define the engine configuration file. 
    /// </summary>
    /// <remarks>
    /// All the pure configuration class in the engine should inherit from this class.
    /// </remarks>
    public class MMEnginePlainConfigurationRoot : IMMPlainConfigurationFileLoader
    {
        public string ConfigurationFilename => "Engine.ini";
    }
}
