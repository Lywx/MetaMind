﻿namespace MetaMind.PerseveranceStarter
{
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using MetaMind.Engine;
    using MetaMind.Perseverance;
    using MetaMind.PerseveranceService.Services;
    using MetaMind.PerseveranceService.Settings;

    public static class Program
    {

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            // Create the ServiceHost.
            using (var host = new ServiceHost(typeof(SynchronizationService), ServiceSettings.Address))
            {
                // Enable metadata publishing.
                var behavior = new ServiceMetadataBehavior
                              {
                                  HttpGetEnabled = true,
                                  MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                              };
                host.Description.Behaviors.Add(behavior);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                using (var engine = new GameEngine())
                {
                    var fullscreen = args.Count() != 0 && args[0] == "--fullscreen";
                    var runner = new Perseverance(engine, fullscreen);

                    runner.Run();
                }

                host.Close();
            }
        }
    }
}