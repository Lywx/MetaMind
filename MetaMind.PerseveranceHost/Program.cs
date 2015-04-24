﻿namespace MetaMind.PerseveranceHost
{
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using MetaMind.Engine;
    using MetaMind.Runtime;
    using MetaMind.RuntimeService.Services;
    using MetaMind.RuntimeService.Settings;

    public static class Program
    {
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
                try
                {
                    host.Open();

                    using (var engine = GameEngine.GetEngine())
                    {
                        var runner = new Runtime(engine);

                        runner.Run();
                    }

                    host.Close();
                }
                catch (AddressAlreadyInUseException)
                {
                    host.Abort();
                }
                catch (CommunicationObjectFaultedException)
                {
                    host.Abort();
                }
            }
        }
    }
}