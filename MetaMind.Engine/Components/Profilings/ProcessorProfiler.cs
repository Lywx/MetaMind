namespace MetaMind.Engine.Components.Profilings
{
    using System;
    using System.Diagnostics;

    /// <reference>
    /// http://stackoverflow.com/questions/1277556/c-sharp-calculate-cpu-usage-for-a-specific-application
    /// http://stackoverflow.com/questions/23391455/performancecounter-reporting-higher-cpu-usage-than-whats-observed
    /// https://msdn.microsoft.com/en-us/library/system.diagnostics.performancecounter.nextvalue.aspx
    /// </reference>
    public class ProcessorProfiler : IDisposable
    {
        private static readonly string ProcessName = Process.GetCurrentProcess().ProcessName;

        private readonly PerformanceCounter cpuCounter;

        private DateTime cpuUsagePercentageMeasuredMoment;

        private double cpuUsagePercentage;

        public ProcessorProfiler()
        { 
            this.cpuCounter = new PerformanceCounter("Process", "% Processor Time", ProcessName);
        }

        public double CPUUsagePercentage()
        {
            if (DateTime.Now - this.cpuUsagePercentageMeasuredMoment < TimeSpan.FromSeconds(1))
            {
                return this.cpuUsagePercentage;
            }

            // http://blogs.msdn.com/b/dotnetinterop/archive/2007/02/02/system-diagnostics-performancecounter-and-processor-time-on-multi-core-or-multi-cpu.aspx
            this.cpuUsagePercentage = this.cpuCounter.NextValue() / Environment.ProcessorCount;
            this.cpuUsagePercentageMeasuredMoment = DateTime.Now;

            return this.cpuUsagePercentage;
        }

        public void Dispose()
        {
            this.cpuCounter.Dispose();
        }
    }
}
