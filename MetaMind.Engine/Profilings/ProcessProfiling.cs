namespace MetaMind.Engine.Profilings
{
    using System.Diagnostics;

    public static class ProcessProfiling
    {
        public static double CPUUsagePercentage(string instanceName)
        {
            // http://stackoverflow.com/questions/1277556/c-sharp-calculate-cpu-usage-for-a-specific-application
            PerformanceCounter cpu = new PerformanceCounter("Process", "% Processor Time", instanceName, true);

            return cpu.NextValue();
        }
    }
}
