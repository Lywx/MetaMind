namespace MetaMind.Acutance
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Engine;
    using MetaMind.PerseveranceService.Settings;

    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress(ServiceSettings.Address);
            var client  = new SynchronizationServiceClient(binding, address)
                              {
                                  InnerChannel = { OperationTimeout = TimeSpan.FromMilliseconds(20) }
                              };

            using (var engine = new GameEngine())
            {
                var fullscreen = args.Count() != 0 && args[0] == "--fullscreen";
                var runner     = new Acutance(engine, client, fullscreen);

                runner.Run();
            }
        }
    }
}