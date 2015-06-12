namespace MetaMind.Testimony.Concepts.Tests
{
    using System;

    public interface ITestComputation
    {
        TimeSpan TestSpan { get; set; }

        Func<bool> TestPassed { get; set; }

        Func<string> TestStatus { get; set; }
    }
}