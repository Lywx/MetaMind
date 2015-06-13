namespace MetaMind.Testimony.Concepts.Tests
{
    using System;

    public interface ITestComputation
    {
        TimeSpan TestSpan { set; }

        Func<bool> TestPassed { set; }

        Func<string> TestStatus { set; }
    }
}