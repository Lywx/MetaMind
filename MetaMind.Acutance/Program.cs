namespace MetaMind.Acutance
{
    using System;
    using System.ServiceModel;

    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Engine;
    using MetaMind.PerseveranceService.Settings;

    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress(ServiceSettings.Address);
            var client = new SynchronizationServiceClient(binding, address)
                             {
                                 InnerChannel = { OperationTimeout = TimeSpan.FromMilliseconds(20) }
                             };

            using (var engine = new GameEngine())
            {
                var runner = new Acutance(engine, client);

                runner.Run();
            }
        }
    }
}